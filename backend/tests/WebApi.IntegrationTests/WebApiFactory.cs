using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace MultiNinja.Backend.WebApi.IntegrationTests;

public sealed class WebApiFactory : WebApplicationFactory<WebApiProgram>, IAsyncLifetime
{
    private readonly WritesDatabaseFixture writesDatabase = WritesDatabaseFixture.Instance;
    
    private readonly ReadsDatabaseFixture readsDatabase = ReadsDatabaseFixture.Instance;

    public async Task InitializeAsync()
    {
        var writesInit = this.writesDatabase.InitializeAsync();
        var readsInit = this.readsDatabase.InitializeAsync();
        await Task.WhenAll(writesInit, readsInit);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        var writesTeardown = this.writesDatabase.DisposeAsync();
        var readsTeardown = this.readsDatabase.DisposeAsync();
        await Task.WhenAll(writesTeardown, readsTeardown);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(_ =>
        {
            // Configure services for tests.
        });
        builder.ConfigureAppConfiguration((_, bld) =>
        {
            var configuration = new Dictionary<string, string?>()
            {
                ["ConnectionStrings:WritesDatabase"] = this.writesDatabase.ConnectionString,
                ["ConnectionStrings:ReadsDatabase"] = this.readsDatabase.ConnectionString,
            };
            bld.AddInMemoryCollection(configuration);
        });

        builder.UseEnvironment("IntegrationTests");
    }
}