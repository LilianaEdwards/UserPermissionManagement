
using UserPermissionManagement.Api.DTOs.Users;
using UserPermissionManagement.Api.Mappings;
using UserPermissionManagement.Api.Models;

namespace UserPermissionManagement.Tests;

public class UserMappingTests
{
    [Fact]
    public void ToEntity_ShouldTrimNameAndEmail_AndSetDefaultValues()
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FullName = "  John Doe  ",
            Email = "  john@example.com  ",
            Password = "Password123!"
        };

        var before = DateTime.UtcNow;

        // Act
        var result = dto.ToEntity();

        var after = DateTime.UtcNow;

        // Assert
        Assert.Equal("John Doe", result.FullName);
        Assert.Equal("john@example.com", result.Email);
        Assert.True(result.IsActive);
        Assert.InRange(result.CreatedAtUtc, before, after);
        Assert.Equal(string.Empty, result.PasswordHash);
    }

    [Fact]
    public void ToResponseDto_ShouldMapUserPropertiesCorrectly()
    {
        // Arrange
        var createdAt = new DateTime(2026, 11, 9, 8, 0, 0, DateTimeKind.Utc);
        var updatedAt = new DateTime(2026, 11, 9, 9, 0, 0, DateTimeKind.Utc);

        var user = new User
        {
            Id = 1,
            FullName = "John Doe",
            Email = "john@example.com",
            IsActive = true,
            CreatedAtUtc = createdAt,
            UpdatedAtUtc = updatedAt
        };

        // Act
        var result = user.ToResponseDto();

        // Assert
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.FullName, result.FullName);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.IsActive, result.IsActive);
        Assert.Equal(createdAt, result.CreatedAtUtc);
        Assert.Equal(updatedAt, result.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateEntity_ShouldUpdateUserPropertiesCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            FullName = "Old Name",
            Email = "old@example.com",
            IsActive = true
        };

        var dto = new UpdateUserDto
        {
            FullName = "  Jane Doe  ",
            Email = "  jane@example.com  ",
            IsActive = false
        };

        var before = DateTime.UtcNow;

        // Act
        user.UpdateEntity(dto);

        var after = DateTime.UtcNow;

        // Assert
        Assert.Equal("Jane Doe", user.FullName);
        Assert.Equal("jane@example.com", user.Email);
        Assert.False(user.IsActive);
        Assert.NotNull(user.UpdatedAtUtc);
        Assert.InRange(user.UpdatedAtUtc.Value, before, after);
    }
}
