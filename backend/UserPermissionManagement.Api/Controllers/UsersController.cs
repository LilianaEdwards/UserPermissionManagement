
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(
            id, cancellationToken);

        if (user is null)
            return NotFound(new
            {
                message = $"User with ID {id} was not found."
            });

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUserDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.CreateAsync(
                dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = user.Id },
                user);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateUserDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.UpdateAsync(
                id, dto, cancellationToken);

            if (user is null)
                return NotFound(new
                {
                    message = $"User with ID {id} was not found."
                });

            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var isDeleted = await _userService.DeleteAsync(
            id, cancellationToken);

        if (!isDeleted)
            return NotFound(new
            {
                message = $"User with ID {id} was not found."
            });

        return NoContent();
    }
}

