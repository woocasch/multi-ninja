using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace MultiNinja.Backend.WebApi.IntegrationTests;

public sealed class WebApiFactory : WebApplicationFactory<WebApiProgram>, IAsyncLifetime
{
    // private readonly WriteDatabaseFixture writeDatabase = WriteDatabaseFixture.Instance;

    public async Task InitializeAsync()
    {
        await Task.Yield();
        // await this.writeDatabase.InitializeAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await Task.Yield();
        // await this.writeDatabase.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            // Configure services for tests.
        });
        // builder.ConfigureAppConfiguration((ctx, bld) =>
        // {
        //     var configuration = new Dictionary<string, string?>()
        //     {
        //         ["ConnectionStrings:WriteDatabase"] = this.writeDatabase.ConnectionString,
        //     };
        //     bld.AddInMemoryCollection(configuration);
        // });

        builder.UseEnvironment("IntegrationTests");
    }
}