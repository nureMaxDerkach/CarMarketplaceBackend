using Application.DTOs.City;

namespace Application.Services.CityService;

public interface ICityService
{
    Task<List<CityDto>> GetCitiesAsync();
}