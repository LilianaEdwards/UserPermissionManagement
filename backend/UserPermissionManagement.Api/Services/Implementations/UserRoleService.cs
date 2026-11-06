
using UserPermissionManagement.Api.Models;
using UserPermissionManagement.Api.Repositories.Interfaces;
using UserPermissionManagement.Api.Services.Interfaces;
using UserPermissionManagement.Api.DTOs.UserRoles;

namespace UserPermissionManagement.Api.Services.Implementations;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<List<UserRoleResponseDto>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            userId, cancellationToken);

        if (user is null)
            throw new KeyNotFoundException(
                $"User with ID {userId} was not found.");

        var userRoles = await _userRoleRepository.GetByUserIdAsync(
            userId, cancellationToken);

        return userRoles
            .Select(userRole => new UserRoleResponseDto
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                RoleName = userRole.Role.Name,
                RoleDescription = userRole.Role.Description
            })
            .ToList();
    }

    public async Task<bool> AssignRoleAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            userId, cancellationToken);

        if (user is null)
            throw new KeyNotFoundException(
                $"User with ID {userId} was not found.");

        var role = await _roleRepository.GetByIdAsync(
            roleId, cancellationToken);

        if (role is null)
            throw new KeyNotFoundException(
                $"Role with ID {roleId} was not found.");

        var existingAssignment =
            await _userRoleRepository.GetByUserAndRoleAsync(
                userId, roleId, cancellationToken);

        if (existingAssignment is not null)
            throw new InvalidOperationException(
                "This role is already assigned to the user.");

        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId
        };

        await _userRoleRepository.AddAsync(
            userRole, cancellationToken);

        return true;
    }

    public async Task<bool> RemoveRoleAsync(
        int userId,
        int roleId,
        CancellationToken cancellationToken = default)
    {
        var existingAssignment =
            await _userRoleRepository.GetByUserAndRoleAsync(
                userId, roleId, cancellationToken);

        if (existingAssignment is null)
            return false;

        return await _userRoleRepository.DeleteAsync(
            userId, roleId, cancellationToken);
    }
}
