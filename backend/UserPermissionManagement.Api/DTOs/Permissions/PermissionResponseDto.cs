namespace UserPermissionManagement.Api.DTOs.Permissions;

public class PermissionResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}