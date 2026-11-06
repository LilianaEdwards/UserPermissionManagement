
namespace UserPermissionManagement.Api.DTOs.UserRoles;

public class UserRoleResponseDto
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? RoleDescription { get; set; }
}
