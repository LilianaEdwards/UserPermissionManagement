
using Microsoft.AspNetCore.Mvc;
using UserPermissionManagement.Api.DTOs.Permissions;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // GET: api/permissions
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var permissions = await _permissionService.GetAllAsync(
            cancellationToken);

        return Ok(permissions);
    }

    // GET: api/permissions/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var permission = await _permissionService.GetByIdAsync(
            id, cancellationToken);

        if (permission is null)
            return NotFound(new
            {
                message = $"Permission with ID {id} was not found."
            });

        return Ok(permission);
    }

    // POST: api/permissions
    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePermissionDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var permission = await _permissionService.CreateAsync(
                dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = permission.Id },
                permission);
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

    // PUT: api/permissions/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePermissionDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var permission = await _permissionService.UpdateAsync(
                id, dto, cancellationToken);

            if (permission is null)
                return NotFound(new
                {
                    message = $"Permission with ID {id} was not found."
                });

            return Ok(permission);
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

    // DELETE: api/permissions/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var isDeleted = await _permissionService.DeleteAsync(
            id, cancellationToken);

        if (!isDeleted)
            return NotFound(new
            {
                message = $"Permission with ID {id} was not found."
            });

        return NoContent();
    }
}
