using UserPermissionManagement.Api.DTOs.Permissions;
using UserPermissionManagement.Api.Models;

namespace UserPermissionManagement.Api.Mappings;

public static class PermissionMapping
{
    public static Permission ToEntity(this CreatePermissionDto dto)
    {
        return new Permission
        {
            Name = dto.Name.Trim(),
            Description = dto.Description
        };
    }

    public static PermissionResponseDto ToResponseDto(this Permission permission)
    {
        return new PermissionResponseDto
        {
            Id = permission.Id,
            Name = permission.Name,
            Description = permission.Description
        };
    }

    public static void UpdateEntity(
        this Permission permission,
        UpdatePermissionDto dto)
    {
        permission.Name = dto.Name.Trim();
        permission.Description = dto.Description;
    }
}