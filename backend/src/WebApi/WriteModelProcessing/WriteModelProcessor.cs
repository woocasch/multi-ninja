using MultiNinja.Backend.Application.WriteModelProcessing;
using MultiNinja.Backend.Infrastructure.Repository.EfCore;

namespace MultiNinja.Backend.WebApi.WriteModelProcessing;

public class WriteModelProcessor : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public WriteModelProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            IProcessor.Result result = IProcessor.Result.None;
            do
            {
                using var scope = this.serviceScopeFactory.CreateScope();
                var processor = scope.ServiceProvider.GetRequiredService<IProcessor>();
                result = await processor.ProcessNextEvent(stoppingToken);
                var writeContext = scope.ServiceProvider.GetRequiredService<WriteContext>();
                await writeContext.SaveChangesAsync(stoppingToken);
            } while (result == IProcessor.Result.EventProcessed);

            await Task.Delay(5000, stoppingToken);
        }
    }
}