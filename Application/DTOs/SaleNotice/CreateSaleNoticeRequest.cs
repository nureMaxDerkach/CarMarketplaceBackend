using Application.DTOs.Car;

namespace Application.DTOs.SaleNotice;

public class CreateSaleNoticeRequest
{
    public int UserId { get; set; }
    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
    
    public CreateCarRequest Car { get; set; }
}