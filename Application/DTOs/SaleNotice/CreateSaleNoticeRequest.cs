using Application.DTOs.Car;

namespace Application.DTOs.SaleNotice;

public class CreateSaleNoticeRequest
{
    public int UserId { get; set; }
    public int ModelId { get; set; }
    public ushort YearOfProduction { get; set; }

    public string Color { get; set; }

    public int Mileage { get; set; }

    public string? Description { get; set; }

    public int Cost { get; set; }

    public string Number { get; set; }
}