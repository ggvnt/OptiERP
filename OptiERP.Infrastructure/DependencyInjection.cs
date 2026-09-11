using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OptiERP.Application.Interfaces;
using OptiERP.Application.Interfaces.Authentication;
using OptiERP.Application.UserCommands.UserRegister;
using OptiERP.Infrastructure.Authentication;
using OptiERP.Infrastructure.Persistence;
using OptiERP.Infrastructure.Repositories;
using OptiERP.Infrastructure.Services;

namespace OptiERP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("PostgreSQL");

        // Database
        services.AddDbContext<OptiErpDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Password Hashing
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Repository
        services.AddScoped<IUserRepository, UserRepository>();

        // JWT Token Generator
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // JWT Settings
        var jwtSettings = new JwtSettings();

        configuration.Bind(
            JwtSettings.SectionName,
            jwtSettings);

        if (string.IsNullOrWhiteSpace(jwtSettings.Secret))
        {
            throw new InvalidOperationException(
                "JWT Secret is not configured.");
        }

        // Register JWT Settings
        services.AddSingleton(jwtSettings);

        // Authentication
        services.AddAuthentication(
            JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    jwtSettings.Secret)),

                        ClockSkew = TimeSpan.Zero
                    };
            });

        return services;
    }
}