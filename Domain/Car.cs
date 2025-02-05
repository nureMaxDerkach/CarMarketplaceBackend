namespace Domain;

public class Car : BaseEntity
{
    public ushort YearOfProduction { get; set; }

    public string Color { get; set; }

    public int Mileage { get; set; }

    public string? Description { get; set; }

    public int Cost { get; set; }

    public string Number { get; set; }

    public int ModelId { get; set; }
    public Model Model { get; set; }
    public ICollection<SaleNotice> SaleNotices { get; set; } = new List<SaleNotice>();

    public ICollection<CarPhoto> CarPhotos { get; set; } = new List<CarPhoto>();
}