namespace Domain;

public class User : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfRegistration { get; set; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; }

    public int CityId { get; set; }

    public City City { get; set; }

    public ICollection<SaleNotice> SaleNotices { get; set; } = new List<SaleNotice>();

    public ICollection<SaleNoticeComment> SaleNoticeComments { get; set; } = new List<SaleNoticeComment>();
}
