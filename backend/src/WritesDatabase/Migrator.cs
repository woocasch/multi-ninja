using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;

namespace MultiNinja.Backend.WritesDatabase;

public class Migrator : BackgroundService
{
    private readonly WriteContext context;

    private readonly ILogger logger;

    public Migrator(WriteContext context, ILogger<Migrator> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.logger.LogInformation("Migrator started");

        var pendingMigrations = await this.context.Database.GetPendingMigrationsAsync(stoppingToken);
        if (pendingMigrations.Any())
        {
            this.logger.LogInformation("Applying pending migrations...");
            await this.context.Database.MigrateAsync(stoppingToken);
            this.logger.LogInformation("Migrations applied successfully.");
        }
        else
        {
            this.logger.LogInformation("No pending migrations found.");
        }

        this.logger.LogInformation("Migrator finished");
    }
}