using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace MultiNinja.Backend.WebApi.IntegrationTests;

public sealed class WebApiFactory : WebApplicationFactory<WebApiProgram>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await Task.Yield();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await Task.Yield();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            // Configure services for tests.
        });
        builder.ConfigureAppConfiguration((ctx, bld) =>
        {
            // var configuration = new Dictionary<string, string?>()
            // {
            //     ["ConnectionStrings:EventsDB"] = this.eventStoreDb.ConnectionString,
            //     ["ConnectionStrings:ReadDB"] = this.mongoDb.ConnectionString,
            //     ["ConnectionStrings:ReadModelDb"] = this.postgreSql.ConnectionString,
            // };
            // bld.AddInMemoryCollection(configuration);
        });

        builder.UseEnvironment("IntegrationTests");
    }
}