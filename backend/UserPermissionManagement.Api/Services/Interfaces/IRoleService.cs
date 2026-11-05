using UserPermissionManagement.Api.DTOs.Roles;

namespace UserPermissionManagement.Api.Services.Interfaces;

public interface IRoleService
{
    Task<List<RoleResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<RoleResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<RoleResponseDto> CreateAsync(
        CreateRoleDto dto,
        CancellationToken cancellationToken = default);

    Task<RoleResponseDto?> UpdateAsync(
        int id,
        UpdateRoleDto dto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}