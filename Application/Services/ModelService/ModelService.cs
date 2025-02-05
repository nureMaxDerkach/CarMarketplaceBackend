using Application.DTOs.Model;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.ModelService;

public class ModelService : IModelService
{
    private readonly DataContext _dataContext;

    public ModelService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<ModelDto>> GetModelsAsync() =>
        await _dataContext.Models
            .Select(m => new ModelDto
            {
                Id = m.Id,
                Name = m.Name,
                BrandId = m.BrandId
            }).ToListAsync();
}