using UserPermissionManagement.Api.DTOs.Roles;
using UserPermissionManagement.Api.Models;

namespace UserPermissionManagement.Api.Mappings;

public static class RoleMapping
{
    public static Role ToEntity(this CreateRoleDto dto)
    {
        return new Role
        {
            Name = dto.Name.Trim(),
            Description = dto.Description
        };
    }

    public static RoleResponseDto ToResponseDto(this Role role)
    {
        return new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
    }

    public static void UpdateEntity(this Role role, UpdateRoleDto dto)
    {
        role.Name = dto.Name.Trim();
        role.Description = dto.Description;
    }
}