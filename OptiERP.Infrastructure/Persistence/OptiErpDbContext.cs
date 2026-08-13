using Microsoft.EntityFrameworkCore;

namespace OptiERP.Infrastructure.Persistence;

public class OptiErpDbContext : DbContext
{
    public OptiErpDbContext(
        DbContextOptions<OptiErpDbContext> options)
        : base(options)
    {
    }
}