using Microsoft.EntityFrameworkCore;
using OptiERP.Domain.Entities;

namespace OptiERP.Infrastructure.Persistence;

public class OptiErpDbContext : DbContext
{
    public OptiErpDbContext(
        DbContextOptions<OptiErpDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }


}