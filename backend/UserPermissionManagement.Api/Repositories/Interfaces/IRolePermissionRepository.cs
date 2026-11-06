using UserPermissionManagement.Api.Models;

namespace UserPermissionManagement.Api.Repositories.Interfaces;

public interface IRolePermissionRepository
{
    Task<List<RolePermission>> GetByRoleIdAsync(
        int roleId,
        CancellationToken cancellationToken = default);

    Task<RolePermission?> GetByRoleAndPermissionAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        RolePermission rolePermission,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default);
}