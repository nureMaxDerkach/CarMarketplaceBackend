using Application.DTOs.SaleNotice;

namespace Application.DTOs.User;

public class UserDetailsDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public int CountryId { get; set; }
    public string Country { get; set; }

    public int RegionId { get; set; }

    public string Region { get; set; }

    public int CityId { get; set; }

    public string City { get; set; }

    public List<UserSaleNoticeDto> SaleNotices { get; set; }
}