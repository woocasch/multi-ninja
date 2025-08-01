using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;
using MultiNinja.Backend.WritesDatabase;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<WriteContext>(options =>
        {
            options.UseMySQL(context.Configuration.GetConnectionString("WriteDatabase")!);
        });

        services.AddHostedService<Migrator>();
    });
    
var host = hostBuilder.Build();
await host.RunAsync();
