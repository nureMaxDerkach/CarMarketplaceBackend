using Application.DTOs.City;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.CityService;

public class CityService : ICityService
{
    private readonly DataContext _dataContext;

    public CityService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<CityDto>> GetCitiesAsync() =>
        await _dataContext.Cities
            .Select(c => new CityDto
            {
                Id = c.Id,
                Name = c.Name,
                RegionId = c.RegionId
            })
            .ToListAsync();
}