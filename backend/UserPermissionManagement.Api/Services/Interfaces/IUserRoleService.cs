
using UserPermissionManagement.Api.DTOs.UserRoles;

namespace UserPermissionManagement.Api.Services.Interfaces;

public interface IUserRoleService
{
    Task<List<UserRoleResponseDto>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);

    Task<bool> AssignRoleAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveRoleAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default);
}
