using Application.DTOs.Car;
using Application.DTOs.User;
using Application.Validation;
using Domain;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Services.UsersService;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    private readonly ILogger<UserService> _logger;

    public UserService(DataContext dataContext, ILogger<UserService> logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public async Task<List<UserDto>> GetUsersAsync() =>
        await _dataContext.Users
            .Where(u => !u.IsDeleted)
            .Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Country = user.City.Region.Country.Name,
                Region = user.City.Region.Name,
                City = user.City.Name
            }).ToListAsync();

    public async Task<UserDetailsDto?> GetUserDetailsByIdAsync(int id) =>
        await _dataContext.Users
            .Where(x => x.Id == id)
            .Select(user => new UserDetailsDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CountryId = user.City.Region.CountryId,
                Country = user.City.Region.Country.Name,
                RegionId = user.City.RegionId,
                Region = user.City.Region.Name,
                CityId = user.CityId,
                City = user.City.Name,
                SaleNotices = user.SaleNotices.Select(saleNotice => new UserSaleNoticeDto
                {
                    Id = saleNotice.Id,
                    DateOfCreation = saleNotice.DateOfCreation,
                    DateOfSale = saleNotice.DateOfSale,
                    Status = saleNotice.Status,
                    Car = new CarDetailsDto
                    {
                        Id = saleNotice.Car.Id,
                        Brand = saleNotice.Car.Model.Brand.Name,
                        Model = saleNotice.Car.Model.Name,
                        YearOfProduction = saleNotice.Car.YearOfProduction,
                        Color = saleNotice.Car.Color,
                        Cost = saleNotice.Car.Cost,
                        Description = saleNotice.Car.Description,
                        Mileage = saleNotice.Car.Mileage,
                        Number = saleNotice.Car.Number
                    }
                }).ToList()
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    
    public async Task<bool> CreateUserAsync(CreateUserRequest request)
    {
        var validationResult = await ValidateCreateUserRequestAsync(request);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Request for creating a user is not valid");
            return false;
        }

        var newUser = CreateUserEntity(request);
        var dbUser = await GetUserByEmailAsync(request.Email);

        if (dbUser is null)
        {
            await _dataContext.Users.AddAsync(newUser);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        switch (dbUser.IsDeleted)
        {
            case false:
                _logger.LogError("Unable to create a user, because there is a user with the same email: {Email}",
                    request.Email);
                return false;
            case true:
                dbUser = newUser;

                _dataContext.Users.Update(dbUser);
                break;
        }

        await _dataContext.SaveChangesAsync();

        return true;
    }

    private static async Task<ValidationResult> ValidateCreateUserRequestAsync(CreateUserRequest request)
    {
        var validator = new CreateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request);
        return validationResult;
    }

    private static User CreateUserEntity(CreateUserRequest request) =>
        new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            CityId = request.CityId
        };

    public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
    {
        var validationResult = await ValidateUpdateUserRequestAsync(request);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Request for updating a user is not valid");
            return false;
        }

        var user = await GetUserByIdAsync(request.Id);
        if (user is null)
        {
            _logger.LogError("Unable to update a user, because there is no user with Id: {UserId}", request.Id);
            return false;
        }

        UpdateUserEntity(user, request);

        _dataContext.Users.Update(user);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        var user = await GetUserByIdAsync(id);

        if (user is null)
        {
            _logger.LogError("Unable to delete a user, because there is no user with id {UserId}", id);
            return false;
        }

        user.IsDeleted = true;

        await _dataContext.SaleNotices.Where(x => x.UserId == id).ExecuteDeleteAsync();
        await _dataContext.SaveChangesAsync();

        return true;
    }

    private static async Task<ValidationResult> ValidateUpdateUserRequestAsync(UpdateUserRequest request)
    {
        var validator = new UpdateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request);
        return validationResult;
    }

    private static void UpdateUserEntity(User user, UpdateUserRequest request)
    {
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        // user.Country = request.Country;
        // user.Region = request.Region;
        // user.City = request.City;
    }

    private async Task<User?> GetUserByIdAsync(int id) => await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

    private async Task<User?> GetUserByEmailAsync(string email) =>
        await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
}