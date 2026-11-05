
using UserPermissionManagement.Api.DTOs.Permissions;

namespace UserPermissionManagement.Api.Services.Interfaces;

public interface IPermissionService
{
    Task<List<PermissionResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<PermissionResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<PermissionResponseDto> CreateAsync(
        CreatePermissionDto dto,
        CancellationToken cancellationToken = default);

    Task<PermissionResponseDto?> UpdateAsync(
        int id,
        UpdatePermissionDto dto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}
