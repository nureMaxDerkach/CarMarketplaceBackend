using Application.Constants;
using Application.DTOs.Report;
using Application.Enums;
using Application.Services.ExcelService;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.ReportService;

public class ReportService : IReportService
{
    private readonly DataContext _dataContext;
    private readonly IExcelService _excelService;

    public ReportService(DataContext dataContext, IExcelService excelService)
    {
        _dataContext = dataContext;
        _excelService = excelService;
    }
    
    public async Task<byte[]> GetSoldCarsReportAsync(TimePeriod timePeriod)
    {
        var startDate = GetStartDate(timePeriod);
        var endDate = GetEndDate(); 
        
        var data = await _dataContext.SaleNotices
            .Where(s => s.Status == SaleNoticeConstants.Sold && s.DateOfSale >= startDate && s.DateOfSale <= endDate)
            .Select(s => new SoldCarsDto
            {
                Brand = s.Car.Model.Brand.Name,
                Model = s.Car.Model.Name,
                Cost = s.Car.Cost.ToString(),
                Number = s.Car.Number.ToString()
            })
            .ToListAsync();
        
        return _excelService.GenerateExcelByteArray(data, $"Sold Cars - {timePeriod.ToString()}");
    }

    private static DateTime GetEndDate() => DateTime.Today.AddDays(1).AddTicks(-1);

    public async Task<byte[]> GetPopularCarsReportAsync(TimePeriod timePeriod)
    {
        var startDate = GetStartDate(timePeriod);
        var endDate = GetEndDate();
        
        var data = await _dataContext.SaleNotices
            .Where(s => s.Status == SaleNoticeConstants.Sold && s.DateOfSale >= startDate && s.DateOfSale <= endDate)
            .GroupBy(s => new { BrandName = s.Car.Model.Brand.Name, ModelName = s.Car.Model.Name })
            .Select(g => new PopularCarsDto
            {
                Brand = g.Key.BrandName,  
                Model = g.Key.ModelName, 
                Quantity = g.Count() 
            })
            .ToListAsync();

        return _excelService.GenerateExcelByteArray(data, $"Popular Cars - {timePeriod.ToString()}");
    }

    private static DateTime GetStartDate(TimePeriod timePeriod)
    {
        DateTime today = DateTime.Today;
        return timePeriod switch
        {
            TimePeriod.Today => today,
            TimePeriod.Week => today.AddDays(-((int)today.DayOfWeek)), 
            TimePeriod.Month => new DateTime(today.Year, today.Month, 1), 
            TimePeriod.Quarter => new DateTime(today.Year, ((today.Month - 1) / 3) * 3 + 1, 1), 
            TimePeriod.Year => new DateTime(today.Year, 1, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(timePeriod), timePeriod, null)
        };
    }

}