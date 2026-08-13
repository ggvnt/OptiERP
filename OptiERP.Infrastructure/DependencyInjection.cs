using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptiERP.Application.Interfaces;
using OptiERP.Infrastructure.Persistence;
using OptiERP.Infrastructure.Services;

namespace OptiERP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("PostgreSQL");
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        services.AddDbContext<OptiErpDbContext>(options =>
            options.UseNpgsql(connectionString));
        return services;
    }
}