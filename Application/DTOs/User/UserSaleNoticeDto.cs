using Application.DTOs.Car;

namespace Application.DTOs.User;

public class UserSaleNoticeDto
{
    public int Id { get; set; }
    
    public DateTime DateOfCreation { get; set; }

    public DateTime? DateOfSale { get; set; }

    public string Status { get; set; }

    public CarDetailsDto Car { get; set; }
}