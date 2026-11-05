
using UserPermissionManagement.Api.DTOs.Roles;
using UserPermissionManagement.Api.Mappings;
using UserPermissionManagement.Api.Models;
using UserPermissionManagement.Api.Repositories.Interfaces;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);

        return roles
            .Select(role => role.ToResponseDto())
            .ToList();
    }

    public async Task<RoleResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetByIdAsync(
            id, cancellationToken);

        return role?.ToResponseDto();
    }

    public async Task<RoleResponseDto> CreateAsync(
        CreateRoleDto dto,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Role name is required.");

        var roleName = dto.Name.Trim();

        var roles = await _roleRepository.GetAllAsync(cancellationToken);

        if (roles.Any(role =>
            role.Name.Equals(
                roleName,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Role name already exists.");
        }

        var role = dto.ToEntity();

        await _roleRepository.AddAsync(role, cancellationToken);

        return role.ToResponseDto();
    }

    public async Task<RoleResponseDto?> UpdateAsync(
        int id,
        UpdateRoleDto dto,
        CancellationToken cancellationToken = default)
    {
        var existingRole = await _roleRepository.GetByIdAsync(
            id, cancellationToken);

        if (existingRole is null)
            return null;

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Role name is required.");

        var roleName = dto.Name.Trim();

        var roles = await _roleRepository.GetAllAsync(cancellationToken);

        if (roles.Any(role =>
            role.Id != id &&
            role.Name.Equals(
                roleName,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Role name already exists.");
        }

        existingRole.UpdateEntity(dto);

        await _roleRepository.UpdateAsync(
            existingRole, cancellationToken);

        return existingRole.ToResponseDto();
    }

    public Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _roleRepository.DeleteAsync(id, cancellationToken);
    }
}
