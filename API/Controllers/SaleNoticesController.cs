using Application.DTOs.SaleNotice;
using Application.Services.SaleNoticesService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SaleNoticesController : BaseApiController
{
    private readonly ISaleNoticesService _saleNoticesService;

    public SaleNoticesController(ISaleNoticesService saleNoticesService)
    {
        _saleNoticesService = saleNoticesService;
    }

    [HttpGet]
    public async Task<ActionResult> GetSaleNotices()
    {
        var result = await _saleNoticesService.GetSaleNoticesAsync();
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSaleNoticeDetailsById(int id)
    {
        var result = await _saleNoticesService.GetSaleNoticeDetailsByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSaleNotice([FromBody] CreateSaleNoticeRequest request)
    {
        var isCreated = await _saleNoticesService.CreateSaleNoticeAsync(request);
        return isCreated ? Created() : BadRequest("Failed to create a sale notice!");
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateSaleNoticeById([FromBody] UpdateSaleNoticeRequest request)
    {
        var isUpdated = await _saleNoticesService.UpdateSaleNoticeAsync(request);
        return isUpdated ? NoContent() : BadRequest("Failed to update a sale notice!");
    }

    [HttpPut("archive/{saleNoticeId:int}")]
    public async Task<IActionResult> ArchiveSaleNoticeById(int saleNoticeId, int userId)
    {
        var isArchived = await _saleNoticesService.ArchiveSaleNoticeByIdAsync(saleNoticeId, userId);
        return isArchived ? NoContent() : BadRequest("Failed to archive a sale notice!");
    }

    [HttpDelete("{saleNoticeId:int}/{userId:int}")]
    public async Task<IActionResult> DeleteSaleNoticeById(int saleNoticeId, int userId)
    {
        var isDeleted = await _saleNoticesService.DeleteSaleNoticeAsync(saleNoticeId, userId);
        return isDeleted ? NoContent() : BadRequest("Failed to remove a sale notice!");
    }
}