namespace Application.DTOs.Car;

public class CarDto
{
    public int Id { get; set; }
    public string Brand { get; set; }

    public string Model { get; set; }

    public ushort YearOfProduction { get; set; }

    public string Color { get; set; }
    
    public int Cost { get; set; }
}