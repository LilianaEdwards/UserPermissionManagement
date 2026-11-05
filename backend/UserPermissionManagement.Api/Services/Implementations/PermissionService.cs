
using UserPermissionManagement.Api.DTOs.Permissions;
using UserPermissionManagement.Api.Mappings;
using UserPermissionManagement.Api.Repositories.Interfaces;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Services.Implementations;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionService(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<List<PermissionResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var permissions = await _permissionRepository.GetAllAsync(
            cancellationToken);

        return permissions
            .Select(permission => permission.ToResponseDto())
            .ToList();
    }

    public async Task<PermissionResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var permission = await _permissionRepository.GetByIdAsync(
            id, cancellationToken);

        return permission?.ToResponseDto();
    }

    public async Task<PermissionResponseDto> CreateAsync(
        CreatePermissionDto dto,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Permission name is required.");

        var permissionName = dto.Name.Trim();

        var permissions = await _permissionRepository.GetAllAsync(
            cancellationToken);

        if (permissions.Any(permission =>
            permission.Name.Equals(
                permissionName,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Permission name already exists.");
        }

        var permission = dto.ToEntity();

        await _permissionRepository.AddAsync(
            permission, cancellationToken);

        return permission.ToResponseDto();
    }

    public async Task<PermissionResponseDto?> UpdateAsync(
        int id,
        UpdatePermissionDto dto,
        CancellationToken cancellationToken = default)
    {
        var existingPermission = await _permissionRepository.GetByIdAsync(
            id, cancellationToken);

        if (existingPermission is null)
            return null;

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Permission name is required.");

        var permissionName = dto.Name.Trim();

        var permissions = await _permissionRepository.GetAllAsync(
            cancellationToken);

        if (permissions.Any(permission =>
            permission.Id != id &&
            permission.Name.Equals(
                permissionName,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Permission name already exists.");
        }

        existingPermission.UpdateEntity(dto);

        await _permissionRepository.UpdateAsync(
            existingPermission, cancellationToken);

        return existingPermission.ToResponseDto();
    }

    public Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _permissionRepository.DeleteAsync(
            id, cancellationToken);
    }
}
