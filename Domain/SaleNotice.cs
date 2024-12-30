namespace Domain;

public class SaleNotice : BaseEntity
{
    public DateTime DateOfCreation { get; set; }

    public DateTime? DateOfSale { get; set; }

    public string Status { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int CarId { get; set; }

    public Car Car { get; set; }
}