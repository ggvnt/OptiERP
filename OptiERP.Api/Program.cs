using OptiERP.Infrastructure;
using OptiERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OptiErpDbContext>();
    
    var canConnect = dbContext.Database.CanConnect();

    Console.WriteLine($"Can connect to the database: {canConnect}");
}

app.Run();

