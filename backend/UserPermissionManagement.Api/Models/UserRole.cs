
namespace UserPermissionManagement.Api.Models;

public class UserRole
{
    // Composite Primary Key
    public int UserId { get; set; }

    public int RoleId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;

    public Role Role { get; set; } = null!;
}
