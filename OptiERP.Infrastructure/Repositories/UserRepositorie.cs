using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OptiERP.Application.Interfaces;
using OptiERP.Application.UserCommands.UserRegister;
using OptiERP.Domain.Entities;
using OptiERP.Infrastructure.Persistence;

namespace OptiERP.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly OptiErpDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public UserRepository(
        OptiErpDbContext dbContext,
        IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<UserRegisterResult>> RegisterUserAsync(
        UserRegisterCommand command,
        CancellationToken cancellationToken = default)
    {
        // Check whether email already exists
        var existingEmail = await _dbContext.Users
            .AnyAsync(
                x => x.Email == command.Email,
                cancellationToken);

        if (existingEmail)
        {
            return Error.Conflict(
                "User.Email",
                "Email already exists.");
        }

        // Check whether username already exists
        var existingUsername = await _dbContext.Users
            .AnyAsync(
                x => x.Username == command.Username,
                cancellationToken);

        if (existingUsername)
        {
            return Error.Conflict(
                "User.Username",
                "Username already exists.");
        }

        // Hash password
        var hashedPassword = _passwordHasher
            .HashPassword(command.Password);

        // Create User domain entity
        var user = User.Create(
            command.Username,
            command.Email,
            hashedPassword);

        // Add user to database
        await _dbContext.Users.AddAsync(
            user,
            cancellationToken);

        // Save changes
        await _dbContext.SaveChangesAsync(
            cancellationToken);

        // Return result
        return new UserRegisterResult(
            user.Id,
            user.Username,
            user.Email,
            user.IsActive,
            user.CreatedAt);
    }
}