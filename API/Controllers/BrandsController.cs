using Application.Services.BrandService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BrandsController : BaseApiController
{
    private readonly IBrandService _brandService;

    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
        var result = await _brandService.GetBrandsAsync();
        return Ok(result);
    }
}