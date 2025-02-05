using Application.DTOs.Region;

namespace Application.Services.RegionService;

public interface IRegionService
{
    Task<List<RegionDto>> GetRegionsAsync();
}