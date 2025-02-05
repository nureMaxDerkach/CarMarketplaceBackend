namespace Domain;

public class CarPhoto : BaseEntity
{
    public string Url { get; set; }

    public int CarId { get; set; }

    public Car Car { get; set; }
}