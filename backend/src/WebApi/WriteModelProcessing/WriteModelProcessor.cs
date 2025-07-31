using MultiNinja.Backend.Application.WriteModelProcessing;

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
            using var scope = this.serviceScopeFactory.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IProcessor>();
            IProcessor.Result result = IProcessor.Result.None;
            do
            {
                try
                {
                    result = await processor.ProcessNextEvent(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (result == IProcessor.Result.EventProcessed);
            await Task.Delay(5000, stoppingToken);
        }
    }
}