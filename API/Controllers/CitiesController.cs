using Application.Services.CityService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CitiesController : BaseApiController
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var result = await _cityService.GetCitiesAsync();
        return Ok(result);
    }
}