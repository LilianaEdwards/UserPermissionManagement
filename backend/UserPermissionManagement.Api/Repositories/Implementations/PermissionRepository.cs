using Microsoft.EntityFrameworkCore;
using UserPermissionManagement.Api.Data;
using UserPermissionManagement.Api.Models;
using UserPermissionManagement.Api.Repositories.Interfaces;

namespace UserPermissionManagement.Api.Repositories.Implementations;

public class PermissionRepository : IPermissionRepository
{
    private readonly AppDbContext _context;

    public PermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.Permissions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<Permission?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Permissions
            .AsNoTracking()
            .FirstOrDefaultAsync(permission => permission.Id == id, cancellationToken);
    }

    public async Task AddAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        await _context.Permissions.AddAsync(permission, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        _context.Permissions.Update(permission);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var permission = await _context.Permissions.FindAsync(new object[] { id }, cancellationToken);

        if (permission is null)
        {
            return false;
        }

        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
