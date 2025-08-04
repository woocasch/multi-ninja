using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;
using Testcontainers.MySql;

namespace MultiNinja.Backend.WebApi.IntegrationTests;

public sealed class ReadsDatabaseFixture : IAsyncLifetime
{
    private static readonly Lazy<ReadsDatabaseFixture> LazyInstance = new(() => new ReadsDatabaseFixture());

    private readonly MySqlContainer container = new MySqlBuilder()
        .WithImage("mysql:8.0")
        .Build();

    private ReadsDatabaseFixture()
    {
    }

    public static ReadsDatabaseFixture Instance => LazyInstance.Value;

    public string ConnectionString => container.GetConnectionString();

    public string ContainerId => $"{container.Id}";

    public async Task InitializeAsync()
    {
        await this.container.StartAsync();
        var optionsBuilder = new DbContextOptionsBuilder<ReadsContext>();
        optionsBuilder.UseMySQL(this.ConnectionString);
        var options = optionsBuilder.Options;
        await using var dbContext = new ReadsContext(options);
        var databaseCreator = dbContext.Database.GetService<IRelationalDatabaseCreator>();
        // needed for idempotency if retrying this method due to transient errors
        await databaseCreator.EnsureDeletedAsync();
        // creates database without schema
        await databaseCreator.CreateAsync();
        // script is not idempotent nor executed in a transaction
        string script = dbContext.Database.GenerateCreateScript().Replace("GO", "");
        await dbContext.Database.ExecuteSqlRawAsync(script);
    }

    public async Task DisposeAsync()
    {
        await this.container.DisposeAsync();
    }
}