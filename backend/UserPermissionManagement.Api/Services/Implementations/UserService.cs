
using Microsoft.AspNetCore.Identity;
using UserPermissionManagement.Api.DTOs.Users;
using UserPermissionManagement.Api.Mappings;
using UserPermissionManagement.Api.Models;
using UserPermissionManagement.Api.Repositories.Interfaces;
using UserPermissionManagement.Api.Services.Interfaces;

namespace UserPermissionManagement.Api.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<UserResponseDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        return users
            .Select(user => user.ToResponseDto())
            .ToList();
    }

    public async Task<UserResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            id, cancellationToken);

        return user?.ToResponseDto();
    }

    public async Task<UserResponseDto> CreateAsync(
        CreateUserDto dto,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.FullName))
            throw new ArgumentException("Full name is required.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email is required.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            throw new ArgumentException("Password is required.");

        var email = dto.Email.Trim();

        var existingUser = await _userRepository.GetByEmailAsync(
            email, cancellationToken);

        if (existingUser is not null)
            throw new InvalidOperationException(
                "Email is already registered.");

        var user = dto.ToEntity();

        user.FullName = dto.FullName.Trim();
        user.Email = email;

        // Hash the password before saving the user.
        user.PasswordHash = _passwordHasher.HashPassword(
            user, dto.Password);

        await _userRepository.AddAsync(user, cancellationToken);

        return user.ToResponseDto();
    }

    public async Task<UserResponseDto?> UpdateAsync(
        int id,
        UpdateUserDto dto,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByIdAsync(
            id, cancellationToken);

        if (existingUser is null)
            return null;

        if (string.IsNullOrWhiteSpace(dto.FullName))
            throw new ArgumentException("Full name is required.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email is required.");

        var email = dto.Email.Trim();

        var emailOwner = await _userRepository.GetByEmailAsync(
            email, cancellationToken);

        if (emailOwner is not null && emailOwner.Id != id)
            throw new InvalidOperationException(
                "Email is already registered.");

        existingUser.UpdateEntity(dto);

        existingUser.FullName = dto.FullName.Trim();
        existingUser.Email = email;

        await _userRepository.UpdateAsync(
            existingUser, cancellationToken);

        return existingUser.ToResponseDto();
    }

    public Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _userRepository.DeleteAsync(id, cancellationToken);
    }
}
