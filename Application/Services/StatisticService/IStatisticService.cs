using Application.DTOs.Statistic;

namespace Application.Services.StatisticService;

public interface IStatisticService
{
    Task<StatisticDto> GetStatisticAsync();
}