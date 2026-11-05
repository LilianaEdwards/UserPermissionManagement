using System.ComponentModel.DataAnnotations;

namespace UserPermissionManagement.Api.DTOs.Roles;

public class UpdateRoleDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }
}