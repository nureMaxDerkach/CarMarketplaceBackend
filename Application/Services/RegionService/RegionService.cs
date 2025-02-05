using Application.DTOs.Region;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.RegionService;

public class RegionService : IRegionService
{
    private readonly DataContext _dataContext;

    public RegionService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<RegionDto>> GetRegionsAsync() => 
        await _dataContext.Regions
        .Select(r => new RegionDto 
            { 
                Id = r.Id, 
                Name = r.Name,
                CountryId = r.CountryId 
            }
        )
        .ToListAsync();
}