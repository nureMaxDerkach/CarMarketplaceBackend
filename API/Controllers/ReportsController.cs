using Application.Enums;
using Application.Services.ReportService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ReportsController : BaseApiController
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }
    
    [HttpGet("soldCars/{timePeriod}")]
    public async Task<IActionResult> GetSoldCarsReport(TimePeriod timePeriod)
    {
        var result = await _reportService.GetSoldCarsReportAsync(timePeriod);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Sold Cars - {timePeriod.ToString()}.xlsx");
    }
    
    [HttpGet("popularCars/{timePeriod}")]
    public async Task<IActionResult> GetPopularCarsReport(TimePeriod timePeriod)
    {
        var result = await _reportService.GetPopularCarsReportAsync(timePeriod);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Popular Cars - {timePeriod.ToString()}.xlsx");
    }
    
}