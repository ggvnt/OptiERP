using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptiERP.Application.Interfaces;
using OptiERP.Application.UserCommands.UserRegister;
using OptiERP.Infrastructure.Persistence;
using OptiERP.Infrastructure.Repositories;
using OptiERP.Infrastructure.Services;

namespace OptiERP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("PostgreSQL");
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserRegisterCommandHandler>();
        
        services.AddDbContext<OptiErpDbContext>(options =>
            options.UseNpgsql(connectionString));
        return services;
    }
}