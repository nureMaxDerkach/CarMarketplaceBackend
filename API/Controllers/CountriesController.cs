using Application.Services.CountryService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CountriesController : BaseApiController
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        var result = await _countryService.GetCountriesAsync();
        return Ok(result);
    }
}