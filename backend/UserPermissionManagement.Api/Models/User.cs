
namespace UserPermissionManagement.Api.Models;

public class User
{
    // Primary Key
    public int Id { get; set; }

    // User Information
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Store hashed password, never plain-text password
    public string PasswordHash { get; set; } = string.Empty;

    // Account Status
    public bool IsActive { get; set; } = true;

    // Audit Information
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtUtc { get; set; }

    // Navigation Property
    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();
}
