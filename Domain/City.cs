namespace Domain;

public class City : BaseEntity
{
    public string Name { get; set; }

    public int RegionId { get; set; }
    public Region Region { get; set; }

    public User User { get; set; }
}