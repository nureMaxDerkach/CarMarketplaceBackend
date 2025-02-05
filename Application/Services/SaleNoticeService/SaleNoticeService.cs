using Application.Constants;
using Application.DTOs.Car;
using Application.DTOs.SaleNotice;
using Application.DTOs.User;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Services.SaleNoticesService;

public class SaleNoticeService : ISaleNoticeService
{
    private readonly DataContext _dataContext;
    private readonly ILogger<SaleNoticeService> _logger;

    public SaleNoticeService(DataContext dataContext, ILogger<SaleNoticeService> logger)
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
                    Brand = notice.Car.Model.Brand.Name,
                    Model = notice.Car.Model.Name,
                    YearOfProduction = notice.Car.YearOfProduction,
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
                    Country = notice.User.City.Region.Country.Name,
                    City = notice.User.City.Name
                },
                Car = new CarDetailsDto
                {
                    Id = notice.Car.Id,
                    BrandId = notice.Car.Model.BrandId, 
                    Brand = notice.Car.Model.Brand.Name,
                    ModelId = notice.Car.ModelId,
                    Model = notice.Car.Model.Name,
                    YearOfProduction = notice.Car.YearOfProduction,
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
            Status = SaleNoticeConstants.Active,
            Car = new Car
            {
                ModelId = request.ModelId,
                YearOfProduction = request.YearOfProduction,
                Color = request.Color,
                Mileage = request.Mileage,
                Description = request.Description,
                Cost = request.Cost,
                Number = request.Number
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
            .FirstOrDefaultAsync(x => x.Id == request.NoticeId && x.UserId == request.UserId);

        if (saleNotice is null)
        {
            _logger.LogError("Unable to update a sale notice, because there is no sale notice with id {SaleNoticeId}",
                request.NoticeId);
            return false;
        }

        saleNotice.DateOfSale = request.DateOfSale;
        saleNotice.Car.Description = request.Description;
        saleNotice.Car.ModelId = request.ModelId;
        saleNotice.Car.Color = request.Color;
        saleNotice.Car.Mileage = request.Mileage;
        saleNotice.Car.Cost = request.Cost;
        saleNotice.Car.YearOfProduction = request.YearOfProduction;
        
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