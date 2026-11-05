
namespace UserPermissionManagement.Api.Models;

public class Permission
{
    // Primary Key
    public int Id { get; set; }

    // Permission Information
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Navigation Property
    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();
}
