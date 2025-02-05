using Application.Services.RegionService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RegionsController : BaseApiController
{
    private readonly IRegionService _regionService;

    public RegionsController(IRegionService regionService)
    {
        _regionService = regionService;
    }


    [HttpGet]
    public async Task<IActionResult> GetRegions()
    {
        var result = await _regionService.GetRegionsAsync();
        return Ok(result);
    }
}