namespace Domain;

public class Model : BaseEntity
{
    public string Name { get; set; }

    public int BrandId { get; set; }
    
    public Brand Brand { get; set; }

    public Car Car { get; set; }
}