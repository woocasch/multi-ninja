using Microsoft.EntityFrameworkCore;
using MultiNinja.Backend.Infrastructure;
using MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;
using MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;
using MultiNinja.Backend.WritesDatabase;

var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddInfrastructure();

builder.Services
    .AddDbContext<WriteContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("WritesDatabase")!,
            x => x.MigrationsAssembly(typeof(WritesDatabaseProgram).Assembly));
    });

builder.Services
    .AddDbContext<ReadsContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("ReadsDatabase")!);
    });

var app = builder.Build();
ApplyWriteContextMigrations(app);


static void ApplyWriteContextMigrations(IHost app)
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

namespace MultiNinja.Backend.WritesDatabase
{
    public sealed partial class WritesDatabaseProgram
    {
    }
}