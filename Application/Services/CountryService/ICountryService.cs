using Application.DTOs.Country;

namespace Application.Services.CountryService;

public interface ICountryService
{
    Task<List<CountryDto>> GetCountriesAsync();
}