namespace UserPermissionManagement.Api.DTOs.RolePermissions;

public class RolePermissionResponseDto
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public string? PermissionDescription { get; set; }
}