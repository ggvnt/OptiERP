using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using OptiERP.Infrastructure;
using OptiERP.Infrastructure.Persistence;
using MediatR;
using OptiERP.Application.UserCommands.UserRegister;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "OptiERP API",
            Version = "v1",
            Description = "OptiERP Backend API"
        });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(UserRegisterCommandHandler).Assembly);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<OptiErpDbContext>();

    var canConnect = dbContext.Database.CanConnect();

    Console.WriteLine(
        $"Can connect to the database: {canConnect}");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();