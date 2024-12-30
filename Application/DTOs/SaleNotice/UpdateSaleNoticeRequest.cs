using Application.DTOs.Car;

namespace Application.DTOs.SaleNotice;

public class UpdateSaleNoticeRequest
{
    public int UserId { get; set; }

    public int SaleNoticeId { get; set; }

    public DateTime? DateOfSale { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public UpdateCarRequest Car { get; set; }
}