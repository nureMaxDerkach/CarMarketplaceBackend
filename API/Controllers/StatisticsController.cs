using Application.Services.StatisticService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StatisticsController : BaseApiController
{
    private readonly IStatisticService _statisticService;

    public StatisticsController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStatistic()
    {
        var result = await _statisticService.GetStatisticAsync();
        return Ok(result);
    }
}