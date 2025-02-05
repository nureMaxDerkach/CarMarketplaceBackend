using Application.DTOs.Car;

namespace Application.DTOs.SaleNotice;

public class UpdateSaleNoticeRequest
{
    public int UserId { get; set; }

    public int NoticeId { get; set; }

    public DateTime? DateOfSale { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public int ModelId { get; set; }

    public ushort YearOfProduction { get; set; }

    public string Color { get; set; }

    public int Mileage { get; set; }

    public string? Description { get; set; }

    public int Cost { get; set; }
}