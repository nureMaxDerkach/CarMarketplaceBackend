using Application.DTOs.SaleNotice;

namespace Application.Services.SaleNoticesService;

public interface ISaleNoticesService
{
    Task<List<SaleNoticeDto>> GetSaleNoticesAsync();

    Task<SaleNoticeDetailsDto?> GetSaleNoticeDetailsByIdAsync(int id);

    Task<bool> CreateSaleNoticeAsync(CreateSaleNoticeRequest request);

    Task<bool> UpdateSaleNoticeAsync(UpdateSaleNoticeRequest request);

    Task<bool> ArchiveSaleNoticeByIdAsync(int saleNoticeId, int userId);

    Task<bool> DeleteSaleNoticeAsync(int saleNoticeId, int userId);
}