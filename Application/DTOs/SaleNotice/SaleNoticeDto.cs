using Application.DTOs.Car;

namespace Application.DTOs.SaleNotice;

public class SaleNoticeDto
{
    public int Id { get; set; }
    
    public DateTime DateOfCreation { get; set; }

    public int UserId { get; set; }

    public CarDto Car { get; set; }
}