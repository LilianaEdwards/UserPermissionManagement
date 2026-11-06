using Microsoft.EntityFrameworkCore;
using UserPermissionManagement.Api.Data;
using UserPermissionManagement.Api.Models;
using UserPermissionManagement.Api.Repositories.Interfaces;

namespace UserPermissionManagement.Api.Repositories.Implementations;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly AppDbContext _context;

    public RolePermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<RolePermission>> GetByRoleIdAsync(
        int roleId,
        CancellationToken cancellationToken = default)
    {
        return _context.RolePermissions
            .AsNoTracking()
            .Include(rolePermission => rolePermission.Permission)
            .Where(rolePermission => rolePermission.RoleId == roleId)
            .ToListAsync(cancellationToken);
    }

    public Task<RolePermission?> GetByRoleAndPermissionAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default)
    {
        return _context.RolePermissions
            .AsNoTracking()
            .FirstOrDefaultAsync(
                rolePermission =>
                    rolePermission.RoleId == roleId &&
                    rolePermission.PermissionId == permissionId,
                cancellationToken);
    }

    public async Task AddAsync(
        RolePermission rolePermission,
        CancellationToken cancellationToken = default)
    {
        await _context.RolePermissions.AddAsync(
            rolePermission,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default)
    {
        var rolePermission = await _context.RolePermissions.FindAsync(
            new object[] { roleId, permissionId },
            cancellationToken);

        if (rolePermission is null)
        {
            return false;
        }

        _context.RolePermissions.Remove(rolePermission);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}