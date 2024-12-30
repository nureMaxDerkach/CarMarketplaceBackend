using Application.DTOs.Car;
using Application.DTOs.User;

namespace Application.DTOs.SaleNotice;

public class SaleNoticeDetailsDto
{
    public int Id { get; set; }
    
    public DateTime DateOfCreation { get; set; }

    public DateTime? DateOfSale { get; set; }

    public string Status { get; set; }

    public UserDto User { get; set; }

    public CarDetailsDto Car { get; set; }
}