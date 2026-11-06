using Microsoft.AspNetCore.Mvc;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Controllers;

[ApiController]
[Route("api/roles/{roleId:int}/permissions")]
public class RolePermissionsController : ControllerBase
{
    private readonly IRolePermissionService _rolePermissionService;

    public RolePermissionsController(
        IRolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
    }

    // GET: api/roles/{roleId}/permissions
    [HttpGet]
    public async Task<IActionResult> GetByRoleId(
        int roleId,
        CancellationToken cancellationToken)
    {
        try
        {
            var permissions =
                await _rolePermissionService.GetByRoleIdAsync(
                    roleId, cancellationToken);

            return Ok(permissions);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST: api/roles/{roleId}/permissions/{permissionId}
    [HttpPost("{permissionId:int}")]
    public async Task<IActionResult> AssignPermission(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken)
    {
        try
        {
            await _rolePermissionService.AssignPermissionAsync(
                roleId, permissionId, cancellationToken);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // DELETE: api/roles/{roleId}/permissions/{permissionId}
    [HttpDelete("{permissionId:int}")]
    public async Task<IActionResult> RemovePermission(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken)
    {
        var isDeleted =
            await _rolePermissionService.RemovePermissionAsync(
                roleId, permissionId, cancellationToken);

        if (!isDeleted)
        {
            return NotFound(new
            {
                message = "Permission assignment was not found."
            });
        }

        return NoContent();
    }
}