namespace Application.DTOs.Car;

public class CarDetailsDto
{
    public int Id { get; set; }

    public int BrandId { get; set; }
    
    public string Brand { get; set; }

    public int ModelId { get; set; }

    public string Model { get; set; }

    public ushort YearOfProduction { get; set; }

    public string Color { get; set; }

    public int Mileage { get; set; }

    public string? Description { get; set; }
    
    public int Cost { get; set; }

    public string Number { get; set; }
}