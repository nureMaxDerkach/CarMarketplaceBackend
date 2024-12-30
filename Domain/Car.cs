namespace Domain;

public class Car : BaseEntity
{
    public string Brand { get; set; }

    public string Model { get; set; }

    public ushort YearOrProduction { get; set; }

    public string Color { get; set; }

    public int Mileage { get; set; }

    public string? Description { get; set; }

    public int Cost { get; set; }

    public string Number { get; set; }

    public ICollection<SaleNotice> SaleNotices { get; set; }
}