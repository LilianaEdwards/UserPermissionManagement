using Microsoft.AspNetCore.Mvc;
using UserPermissionManagement.Api.Services.Interfaces;
using UserPermissionManagement.Api.DTOs.Roles;

namespace UserPermissionManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllAsync(cancellationToken);

        return Ok(roles);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var role = await _roleService.GetByIdAsync(
            id, cancellationToken);

        if (role is null)
            return NotFound(new
            {
                message = $"Role with ID {id} was not found."
            });

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateRoleDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleService.CreateAsync(
                dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = role.Id },
                role);
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
        UpdateRoleDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleService.UpdateAsync(
                id, dto, cancellationToken);

            if (role is null)
                return NotFound(new
                {
                    message = $"Role with ID {id} was not found."
                });

            return Ok(role);
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
        var isDeleted = await _roleService.DeleteAsync(
            id, cancellationToken);

        if (!isDeleted)
            return NotFound(new
            {
                message = $"Role with ID {id} was not found."
            });

        return NoContent();
    }
}