using Application.Enums;

namespace Application.Services.ReportService;

public interface IReportService
{
    Task<byte[]> GetSoldCarsReportAsync(TimePeriod timePeriod);

    Task<byte[]> GetPopularCarsReportAsync(TimePeriod timePeriod);
}