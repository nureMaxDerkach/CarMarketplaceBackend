using Application.DTOs.User;
using Application.Services.UsersService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _usersService.GetUsersAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserDetailsById(int id)
    {
        var result = await _usersService.GetUserDetailsByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var isCreated = await _usersService.CreateUserAsync(request);
        return isCreated ? Created() : BadRequest("Failed to create a user account!");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var isUpdated = await _usersService.UpdateUserAsync(request);
        return isUpdated ? NoContent() : BadRequest("Failed to update a user a account!");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        var isDeleted = await _usersService.DeleteUserByIdAsync(id);
        return isDeleted ? NoContent() : BadRequest("Failed to delete a user account!");
    }
}