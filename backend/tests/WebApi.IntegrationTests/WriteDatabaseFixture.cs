using Testcontainers.MySql;

namespace MultiNinja.Backend.WebApi.IntegrationTests;

public sealed class WriteDatabaseFixture: IAsyncLifetime
{
    private static readonly Lazy<WriteDatabaseFixture> LazyInstance = new(() => new WriteDatabaseFixture());

    private readonly MySqlContainer container = new MySqlBuilder()
        .WithImage("mysql:8.0")
        .Build();

    private WriteDatabaseFixture()
    {
    }

    public static WriteDatabaseFixture Instance => LazyInstance.Value;

    public string ConnectionString => container.GetConnectionString();

    public string ContainerId => $"{container.Id}";

    public async Task InitializeAsync()
    {
        await this.container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await this.container.DisposeAsync();
    }

}