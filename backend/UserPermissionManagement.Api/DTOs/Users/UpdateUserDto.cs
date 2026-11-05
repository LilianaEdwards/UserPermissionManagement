using System.ComponentModel.DataAnnotations;

namespace UserPermissionManagement.Api.DTOs.Users;

public class UpdateUserDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}