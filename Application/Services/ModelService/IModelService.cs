using Application.DTOs.Model;

namespace Application.Services.ModelService;

public interface IModelService
{
    Task<List<ModelDto>> GetModelsAsync();
}