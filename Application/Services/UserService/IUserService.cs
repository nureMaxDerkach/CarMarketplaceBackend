using Application.DTOs.User;

namespace Application.Services.UsersService;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();

    Task<UserDetailsDto?> GetUserDetailsByIdAsync(int id);

    Task<bool> CreateUserAsync(CreateUserRequest request);

    Task<bool> UpdateUserAsync(UpdateUserRequest request);

    Task<bool> DeleteUserByIdAsync(int id);
}