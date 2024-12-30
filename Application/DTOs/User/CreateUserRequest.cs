namespace Application.DTOs.User;

public class CreateUserRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Country { get; set; }

    public string Region { get; set; }

    public string City { get; set; }
}