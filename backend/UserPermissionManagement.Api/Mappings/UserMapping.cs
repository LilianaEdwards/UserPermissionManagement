
using UserPermissionManagement.Api.DTOs.Users;
using UserPermissionManagement.Api.Models;

namespace UserPermissionManagement.Api.Mappings;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto dto)
    {
        return new User
        {
            FullName = dto.FullName.Trim(),
            Email = dto.Email.Trim(),
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public static UserResponseDto ToResponseDto(this User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            IsActive = user.IsActive,
            CreatedAtUtc = user.CreatedAtUtc,
            UpdatedAtUtc = user.UpdatedAtUtc
        };
    }

    public static void UpdateEntity(
        this User user,
        UpdateUserDto dto)
    {
        user.FullName = dto.FullName.Trim();
        user.Email = dto.Email.Trim();
        user.IsActive = dto.IsActive;
        user.UpdatedAtUtc = DateTime.UtcNow;
    }
}
