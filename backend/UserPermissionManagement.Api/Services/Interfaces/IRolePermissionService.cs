using UserPermissionManagement.Api.DTOs.RolePermissions;

namespace UserPermissionManagement.Api.Services.Interfaces;

public interface IRolePermissionService
{
    Task<List<RolePermissionResponseDto>> GetByRoleIdAsync(
        int roleId,
        CancellationToken cancellationToken = default);

    Task<bool> AssignPermissionAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default);

    Task<bool> RemovePermissionAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default);
}