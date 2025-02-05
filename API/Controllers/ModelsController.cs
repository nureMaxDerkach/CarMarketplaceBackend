using Application.Services.ModelService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ModelsController : BaseApiController
{
    private readonly IModelService _modelService;

    public ModelsController(IModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetModels()
    {
        var result = await _modelService.GetModelsAsync();
        return Ok(result);
    }
}