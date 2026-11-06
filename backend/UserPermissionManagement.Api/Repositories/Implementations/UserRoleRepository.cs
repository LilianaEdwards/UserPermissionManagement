
using Microsoft.EntityFrameworkCore;
using UserPermissionManagement.Api.Data;
using UserPermissionManagement.Api.Models;
using UserPermissionManagement.Api.Repositories.Interfaces;

namespace UserPermissionManagement.Api.Repositories.Implementations;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly AppDbContext _context;

    public UserRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<UserRole>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return _context.UserRoles
            .AsNoTracking()
            .Include(userRole => userRole.Role)
            .Where(userRole => userRole.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public Task<UserRole?> GetByUserAndRoleAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default)
    {
        return _context.UserRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(
                userRole => userRole.UserId == userId
                    && userRole.RoleId == roleId,
                cancellationToken);
    }

    public async Task AddAsync(
        UserRole userRole,
        CancellationToken cancellationToken = default)
    {
        await _context.UserRoles.AddAsync(
            userRole,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default)
    {
        var userRole = await _context.UserRoles.FindAsync(
            new object[] { userId, roleId },
            cancellationToken);

        if (userRole is null)
        {
            return false;
        }

        _context.UserRoles.Remove(userRole);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
