namespace Domain;

public class SaleNotice : BaseEntity
{
    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

    public DateTime? DateOfSale { get; set; }

    public string Status { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }

    public User User { get; set; }

    public int CarId { get; set; }

    public Car Car { get; set; }

    public ICollection<SaleNoticeComment> SaleNoticeComments { get; set; } = new List<SaleNoticeComment>();
}