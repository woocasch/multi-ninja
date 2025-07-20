using MultiNinja.Backend.Application.WriteModelProcessing;

namespace MultiNinja.Backend.WebApi.WriteModelProcessing;

public class WriteModelProcessor : BackgroundService
{
    private readonly IProcessor processor;

    public WriteModelProcessor(IProcessor processor)
    {
        this.processor = processor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            IProcessor.Result result;
            do
            {
                result = await this.processor.ProcessNextEvent(stoppingToken);
            } while (result == IProcessor.Result.EventProcessed);
            await Task.Delay(1000, stoppingToken);
        }
    }
}