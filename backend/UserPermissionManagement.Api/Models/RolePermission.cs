
namespace UserPermissionManagement.Api.Models;

public class RolePermission
{
    // Composite Primary Key
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    // Navigation Properties
    public Role Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;
}
