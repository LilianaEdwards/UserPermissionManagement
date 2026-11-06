using UserPermissionManagement.Api.DTOs.RolePermissions;
using UserPermissionManagement.Api.Models;
using UserPermissionManagement.Api.Repositories.Interfaces;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Services.Implementations;

public class RolePermissionService : IRolePermissionService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public RolePermissionService(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<List<RolePermissionResponseDto>> GetByRoleIdAsync(
        int roleId,
        CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetByIdAsync(
            roleId, cancellationToken);

        if (role is null)
            throw new KeyNotFoundException(
                $"Role with ID {roleId} was not found.");

        var rolePermissions =
            await _rolePermissionRepository.GetByRoleIdAsync(
                roleId, cancellationToken);

        return rolePermissions
            .Select(rolePermission => new RolePermissionResponseDto
            {
                RoleId = rolePermission.RoleId,
                PermissionId = rolePermission.PermissionId,
                PermissionName = rolePermission.Permission.Name,
                PermissionDescription = rolePermission.Permission.Description
            })
            .ToList();
    }

    public async Task<bool> AssignPermissionAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetByIdAsync(
            roleId, cancellationToken);

        if (role is null)
            throw new KeyNotFoundException(
                $"Role with ID {roleId} was not found.");

        var permission = await _permissionRepository.GetByIdAsync(
            permissionId, cancellationToken);

        if (permission is null)
            throw new KeyNotFoundException(
                $"Permission with ID {permissionId} was not found.");

        var existingAssignment =
            await _rolePermissionRepository.GetByRoleAndPermissionAsync(
                roleId, permissionId, cancellationToken);

        if (existingAssignment is not null)
            throw new InvalidOperationException(
                "This permission is already assigned to the role.");

        var rolePermission = new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        };

        await _rolePermissionRepository.AddAsync(
            rolePermission, cancellationToken);

        return true;
    }

    public async Task<bool> RemovePermissionAsync(
        int roleId,
        int permissionId,
        CancellationToken cancellationToken = default)
    {
        var existingAssignment =
            await _rolePermissionRepository.GetByRoleAndPermissionAsync(
                roleId, permissionId, cancellationToken);

        if (existingAssignment is null)
            return false;

        return await _rolePermissionRepository.DeleteAsync(
            roleId, permissionId, cancellationToken);
    }
}