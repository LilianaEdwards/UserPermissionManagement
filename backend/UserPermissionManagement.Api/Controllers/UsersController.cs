
using Microsoft.AspNetCore.Mvc;
using UserPermissionManagement.Api.DTOs.Users;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken);

        return Ok(users);
    }
}

