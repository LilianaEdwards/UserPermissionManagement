
using Microsoft.AspNetCore.Mvc;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Controllers;

[ApiController]
[Route("api/users/{userId:int}/roles")]
public class UserRolesController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRolesController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    // GET: api/users/{userId}/roles
    [HttpGet]
    public async Task<IActionResult> GetByUserId(
        int userId,
        CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _userRoleService.GetByUserIdAsync(
                userId, cancellationToken);

            return Ok(roles);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST: api/users/{userId}/roles/{roleId}
    [HttpPost("{roleId:int}")]
    public async Task<IActionResult> AssignRole(
        int userId,
        int roleId,
        CancellationToken cancellationToken)
    {
        try
        {
            await _userRoleService.AssignRoleAsync(
                userId, roleId, cancellationToken);

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

    // DELETE: api/users/{userId}/roles/{roleId}
    [HttpDelete("{roleId:int}")]
    public async Task<IActionResult> RemoveRole(
        int userId,
        int roleId,
        CancellationToken cancellationToken)
    {
        var isDeleted = await _userRoleService.RemoveRoleAsync(
            userId, roleId, cancellationToken);

        if (!isDeleted)
        {
            return NotFound(new
            {
                message = "Role assignment was not found."
            });
        }

        return NoContent();
    }
}
