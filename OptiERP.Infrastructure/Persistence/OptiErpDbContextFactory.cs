using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OptiERP.Infrastructure.Persistence;

public class OptiErpDbContextFactory
    : IDesignTimeDbContextFactory<OptiErpDbContext>
{
    public OptiErpDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<OptiErpDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=OptiERP;Username=postgres;Password=12345"
        );

        return new OptiErpDbContext(optionsBuilder.Options);
    }
}