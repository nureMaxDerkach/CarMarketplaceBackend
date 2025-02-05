using Application.Constants;
using Application.DTOs.Statistic;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.StatisticService;

public class StatisticService : IStatisticService
{
    private readonly DataContext _dataContext;

    public StatisticService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<StatisticDto> GetStatisticAsync()
    {
        var activeSaleNoticesCount = await GetActiveSaleNoticesCountAsync();
        var soldCarsCount = await GetSoldCarsCountAsync();
        var activeUsersCount = await GetActiveUsersCountAsync();
        var popularBrand = await GetTheMostPopularCarBrand();
        
        return new StatisticDto
        {
            ActiveSaleNoticesCount = activeSaleNoticesCount,
            SoldCarsCount = soldCarsCount,
            ActiveUsersCount = activeUsersCount,
            PopularBrand = popularBrand
        };
    }
    
    private async Task<int> GetActiveSaleNoticesCountAsync() =>
        await _dataContext.SaleNotices
            .Where(s => s.Status == SaleNoticeConstants.Active)
            .CountAsync();

    private async Task<int> GetSoldCarsCountAsync() =>
        await _dataContext.SaleNotices
            .Where(s => s.Status == SaleNoticeConstants.Sold)
            .CountAsync();

    private async Task<int> GetActiveUsersCountAsync() => 
        await _dataContext.Users
            .Where(u => !u.IsDeleted)
            .CountAsync();

    private async Task<string> GetTheMostPopularCarBrand()
    {
        var popularBrand = await _dataContext.SaleNotices
            .Where(s => s.Status == SaleNoticeConstants.Sold) 
            .GroupBy(s => s.Car.Model.Brand.Name)
            .Select(group => new
            {
                Brand = group.Key,
                SoldCount = group.Count()
            })
            .OrderByDescending(x => x.SoldCount) 
            .FirstOrDefaultAsync(); 
        
        return popularBrand?.Brand ?? "No Data";
    }
}