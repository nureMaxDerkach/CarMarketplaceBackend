using Application.DTOs.User;
using Application.Services.UsersService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserDetailsById(int id)
    {
        var result = await _userService.GetUserDetailsByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var isCreated = await _userService.CreateUserAsync(request);
        return isCreated ? Created() : BadRequest("Failed to create a user account!");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var isUpdated = await _userService.UpdateUserAsync(request);
        return isUpdated ? NoContent() : BadRequest("Failed to update a user a account!");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        var isDeleted = await _userService.DeleteUserByIdAsync(id);
        return isDeleted ? NoContent() : BadRequest("Failed to delete a user account!");
    }
}