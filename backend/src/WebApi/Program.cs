using Microsoft.EntityFrameworkCore;
using MultiNinja.Backend.Infrastructure;
using MultiNinja.Backend.Infrastructure.WriteModel.EfCore;
using MultiNinja.Backend.WebApi.Endpoints;
using MultiNinja.Backend.WebApi.Orchestration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddInfrastructure()
    .AddOrchestration()
    .AddHostedService<MultiNinja.Backend.WebApi.WriteModelProcessing.WriteModelProcessor>();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services
    .AddDbContext<WriteContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("WriteDatabase")!);
    })
    .AddDbContextFactory<WriteContext>(_ => { }, ServiceLifetime.Scoped);

builder.Services.AddOpenApi();

var app = builder.Build();

ApplyWriteContextMigrations(app);

app
    .MapAuth()
    .MapOpenApi();
app
    .MapScalarApiReference();

app.Run();

static void ApplyWriteContextMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<WriteContext>();

    // Check and apply pending migrations
    var pendingMigrations = dbContext.Database.GetPendingMigrations();
    if (pendingMigrations.Any())
    {
        Console.WriteLine("Applying pending migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations applied successfully.");
    }
    else
    {
        Console.WriteLine("No pending migrations found.");
    }
}

namespace MultiNinja.Backend.WebApi
{
    public sealed partial class WebApiProgram
    {
    }
}