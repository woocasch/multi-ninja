using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiNinja.Backend.Infrastructure;
using MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;
using MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;
using MultiNinja.Backend.ReadsDatabase;

var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddInfrastructure();

builder.Services
    .AddDbContext<WriteContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("WritesDatabase")!);
    });

builder.Services
    .AddDbContext<ReadsContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("ReadsDatabase")!,
            x => x.MigrationsAssembly(typeof(ReadsDatabaseProgram).Assembly));
    });

var app = builder.Build();
ApplyReadContextMigrations(app);


static void ApplyReadContextMigrations(IHost app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ReadsContext>();

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

namespace MultiNinja.Backend.ReadsDatabase
{
    public sealed partial class ReadsDatabaseProgram
    {
    }
}