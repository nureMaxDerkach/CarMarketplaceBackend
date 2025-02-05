using Application.DTOs.Brand;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.BrandService;

public class BrandService : IBrandService
{
    private readonly DataContext _dataContext;

    public BrandService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<BrandDto>> GetBrandsAsync() =>
        await _dataContext.Brands
            .Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name
            })
            .ToListAsync();
}