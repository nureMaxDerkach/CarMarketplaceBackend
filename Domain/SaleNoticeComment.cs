namespace Domain;

public class SaleNoticeComment : BaseEntity
{
    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
    public string Comment { get; set; }
    public DateTime LastUpdated { get; set; }
    public int SaleNoticeId { get; set; }
    public SaleNotice SaleNotice { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}