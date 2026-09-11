using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OptiERP.Application.Interfaces;
using OptiERP.Application.Interfaces.Authentication;
using OptiERP.Application.UserCommands.GetCurrentUser;
using OptiERP.Application.UserCommands.GetUserById;
using OptiERP.Application.UserCommands.Login.Normal;
using OptiERP.Application.UserCommands.UserRegister;
using OptiERP.Domain.Entities;
using OptiERP.Infrastructure.Persistence;

namespace OptiERP.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly OptiErpDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserRepository(
        OptiErpDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
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

        var token = _jwtTokenGenerator.GenerateToken(user);

        // Return result
        return new UserRegisterResult(
            user.Id,
            user.Username,
            user.Email,
            user.IsActive,
            user.CreatedAt,
            token);
    }

    public async Task<ErrorOr<UserLoginResult>> LoginUserAsync(
        UserLoginCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(
                x => x.Email == command.Email,
                cancellationToken);
        
        if (user is null)
        {
            return Error.NotFound(
                "User.Email",
                "User with the provided email does not exist.");
        }

        var passwordValid = _passwordHasher.VerifyPassword(
            command.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            return Error.Unauthorized(
                "User.Password",
                "Invalid password.");
        }

        if (!user.IsActive)
        {
            return Error.Validation(
                "User.IsActive",
                "User account is not active.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new UserLoginResult(
            token,
            user.Id,
            user.Email);
    }

    public async Task<ErrorOr<GetUserByIdResult>> GetUserByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(
                x => x.Id == userId,
                cancellationToken);

        if (user is null)
        {
            return Error.NotFound(
                "User.Id",
                "User with the provided ID does not exist.");
        }

        return new GetUserByIdResult(
            user.Id,
            user.Username,
            user.Email,
            user.IsActive,
            user.CreatedAt);
    }
}