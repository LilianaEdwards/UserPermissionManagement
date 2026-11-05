using Microsoft.EntityFrameworkCore;
using UserPermissionManagement.Api.Models;

namespace UserPermissionManagement.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);
        });

        // Role configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
            entity.HasIndex(r => r.Name).IsUnique();
            entity.Property(r => r.Description).HasMaxLength(200);
        });

        // Permission configuration
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(p => p.Name).IsUnique();
            entity.Property(p => p.Description).HasMaxLength(200);
        });

        // UserRole many-to-many relationship
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        });

        // RolePermission many-to-many relationship
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            entity.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            entity.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
        });

        // Seed Roles
    modelBuilder.Entity<Role>().HasData(
        new Role
        {
            Id = 1,
            Name = "Admin",
            Description = "Full system access"
        },
        new Role
        {
            Id = 2,
            Name = "Manager",
            Description = "Manage and view users"
        },
        new Role
        {
            Id = 3,
            Name = "Employee",
            Description = "Access own profile"
        }
    );

        // Seed Permissions
        modelBuilder.Entity<Permission>().HasData(
            new Permission { Id = 1, Name = "Users.ReadAll", Description = "View all users" },
            new Permission { Id = 2, Name = "Users.ReadOwn", Description = "View own profile" },
            new Permission { Id = 3, Name = "Users.Create", Description = "Create users" },
            new Permission { Id = 4, Name = "Users.Update", Description = "Update users" },
            new Permission { Id = 5, Name = "Users.Delete", Description = "Delete users" },
            new Permission { Id = 6, Name = "Roles.Read", Description = "View roles" },
            new Permission { Id = 7, Name = "Roles.Manage", Description = "Manage roles" },
            new Permission { Id = 8, Name = "Permissions.Read", Description = "View permissions" },
            new Permission { Id = 9, Name = "Permissions.Manage", Description = "Manage permissions" }
        );
        // Seed Role-Permission relationships
        modelBuilder.Entity<RolePermission>().HasData(
            // Admin: all permissions
            new RolePermission { RoleId = 1, PermissionId = 1 },
            new RolePermission { RoleId = 1, PermissionId = 2 },
            new RolePermission { RoleId = 1, PermissionId = 3 },
            new RolePermission { RoleId = 1, PermissionId = 4 },
            new RolePermission { RoleId = 1, PermissionId = 5 },
            new RolePermission { RoleId = 1, PermissionId = 6 },
            new RolePermission { RoleId = 1, PermissionId = 7 },
            new RolePermission { RoleId = 1, PermissionId = 8 },
            new RolePermission { RoleId = 1, PermissionId = 9 },

            // Manager: user management and read access
            new RolePermission { RoleId = 2, PermissionId = 1 },
            new RolePermission { RoleId = 2, PermissionId = 2 },
            new RolePermission { RoleId = 2, PermissionId = 3 },
            new RolePermission { RoleId = 2, PermissionId = 4 },
            new RolePermission { RoleId = 2, PermissionId = 6 },
            new RolePermission { RoleId = 2, PermissionId = 8 },

            // Employee: own profile only
            new RolePermission { RoleId = 3, PermissionId = 2 }
        );
    }
}