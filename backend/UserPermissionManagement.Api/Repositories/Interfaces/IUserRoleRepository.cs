using UserPermissionManagement.Api.Models;

namespace UserPermissionManagement.Api.Repositories.Interfaces;

public interface IUserRoleRepository
{
    Task<List<UserRole>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);

    Task<UserRole?> GetByUserAndRoleAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        UserRole userRole,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default);
}