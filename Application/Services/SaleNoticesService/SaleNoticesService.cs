using Application.Constants;
using Application.DTOs.Car;
using Application.DTOs.SaleNotice;
using Application.DTOs.User;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Services.SaleNoticesService;

public class SaleNoticesService : ISaleNoticesService
{
    private readonly DataContext _dataContext;
    private readonly ILogger<SaleNoticesService> _logger;

    public SaleNoticesService(DataContext dataContext, ILogger<SaleNoticesService> logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public async Task<List<SaleNoticeDto>> GetSaleNoticesAsync() =>
        await _dataContext.SaleNotices
            .Where(x => x.Status == SaleNoticeConstants.Active)
            .Select(notice => new SaleNoticeDto
            {
                Id = notice.Id,
                DateOfCreation = notice.DateOfCreation,
                UserId = notice.UserId,
                Car = new CarDto
                {
                    Id = notice.Car.Id,
                    Brand = notice.Car.Brand,
                    Model = notice.Car.Model,
                    YearOfProduction = notice.Car.YearOrProduction,
                    Color = notice.Car.Color,
                    Cost = notice.Car.Cost,
                }
            })
            .AsNoTracking()
            .ToListAsync();

    public async Task<SaleNoticeDetailsDto?> GetSaleNoticeDetailsByIdAsync(int id)
    {
        var saleNotice = await _dataContext.SaleNotices
            .Select(notice => new SaleNoticeDetailsDto()
            {
                Id = notice.Id,
                DateOfCreation = notice.DateOfCreation,
                DateOfSale = notice.DateOfSale,
                Status = notice.Status,
                User = new UserDto
                {
                    Id = notice.UserId,
                    FirstName = notice.User.FirstName,
                    LastName = notice.User.LastName,
                    Email = notice.User.Email,
                    PhoneNumber = notice.User.Email,
                    Country = notice.User.Country,
                    City = notice.User.City
                },
                Car = new CarDetailsDto
                {
                    Id = notice.Car.Id,
                    Brand = notice.Car.Brand,
                    Model = notice.Car.Model,
                    YearOfProduction = notice.Car.YearOrProduction,
                    Color = notice.Car.Color,
                    Cost = notice.Car.Cost,
                    Description = notice.Car.Description,
                    Mileage = notice.Car.Mileage,
                    Number = notice.Car.Number
                }
            }).FirstOrDefaultAsync(x => x.Id == id);

        if (saleNotice is not null)
        {
            return saleNotice;
        }
        
        _logger.LogError("Their is no a sale notice with id {SaleNoticeId}", id);
        return null;
    }

    public async Task<bool> CreateSaleNoticeAsync(CreateSaleNoticeRequest request)
    {
        var isUserExist = await IsUserExistAsync(request.UserId);

        if (!isUserExist)
        {
            _logger.LogError("Unable to create a sale notice, because there is no user with id {UserId}",
                request.UserId);
            return false;
        }

        var newSaleNotice = new SaleNotice
        {
            UserId = request.UserId,
            DateOfCreation = request.DateOfCreation,
            Status = SaleNoticeConstants.Active,
            Car = new Car
            {
                Brand = request.Car.Brand,
                Model = request.Car.Model,
                YearOrProduction = request.Car.YearOrProduction,
                Color = request.Car.Color,
                Mileage = request.Car.Mileage,
                Description = request.Car.Description,
                Cost = request.Car.Cost,
                Number = request.Car.Number
            }
        };

        await _dataContext.AddAsync(newSaleNotice);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateSaleNoticeAsync(UpdateSaleNoticeRequest request)
    {
        var saleNotice = await _dataContext.SaleNotices
            .Include(x => x.Car)
            .FirstOrDefaultAsync(x => x.Id == request.SaleNoticeId && x.UserId == request.UserId);

        if (saleNotice is null)
        {
            _logger.LogError("Unable to update a sale notice, because there is no sale notice with id {SaleNoticeId}",
                request.SaleNoticeId);
            return false;
        }

        saleNotice.DateOfSale = request.DateOfSale;
        saleNotice.Car.Description = request.Car.Description;
        saleNotice.Car.Brand = request.Car.Brand;
        saleNotice.Car.Model = request.Car.Model;
        saleNotice.Car.Color = request.Car.Color;
        saleNotice.Car.Mileage = request.Car.Mileage;
        saleNotice.Car.Cost = request.Car.Cost;
        saleNotice.Car.YearOrProduction = request.Car.YearOrProduction;
        
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ArchiveSaleNoticeByIdAsync(int saleNoticeId, int userId)
    {
        var saleNotice = await _dataContext.SaleNotices.FirstOrDefaultAsync(x => x.Id == saleNoticeId && x.UserId == userId);
        if (saleNotice is null)
        {
            _logger.LogError("Unable to archive a sale notice, because there is no sale notice with id {SaleNoticeId}",
                saleNoticeId);
            return false;
        }
        
        if (saleNotice.Status == SaleNoticeConstants.Archived)
        {
            _logger.LogInformation("Sale notice {SaleNoticeId} is already archived.", saleNoticeId);
            return true;
        }

        saleNotice.Status = SaleNoticeConstants.Archived;
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteSaleNoticeAsync(int saleNoticeId, int userId)
    {
        var saleNotice = await _dataContext.SaleNotices.FirstOrDefaultAsync(x => x.Id == saleNoticeId && x.UserId == userId);
        if (saleNotice is null)
        {
            _logger.LogError("Unable to delete a sale notice, because there is no sale notice with id {SaleNoticeId}",
                saleNoticeId);
            return false;
        }

        _dataContext.Remove(saleNotice);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    private async Task<bool> IsUserExistAsync(int userId) => await _dataContext.Users.AnyAsync(x => x.Id == userId);
}