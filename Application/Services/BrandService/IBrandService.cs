using Application.DTOs.Brand;

namespace Application.Services.BrandService;

public interface IBrandService
{
    Task<List<BrandDto>> GetBrandsAsync();
}