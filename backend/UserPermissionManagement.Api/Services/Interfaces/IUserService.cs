using UserPermissionManagement.Api.DTOs.Users;

namespace UserPermissionManagement.Api.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<UserResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<UserResponseDto> CreateAsync(
        CreateUserDto dto,
        CancellationToken cancellationToken = default);

    Task<UserResponseDto?> UpdateAsync(
        int id,
        UpdateUserDto dto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}