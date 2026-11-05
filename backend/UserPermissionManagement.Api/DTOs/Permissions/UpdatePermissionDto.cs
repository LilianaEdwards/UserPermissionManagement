using System.ComponentModel.DataAnnotations;

namespace UserPermissionManagement.Api.DTOs.Permissions;

public class UpdatePermissionDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }
}