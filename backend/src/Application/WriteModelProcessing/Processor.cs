using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.Repository;

namespace MultiNinja.Backend.Application.WriteModelProcessing;

public class Processor : IProcessor
{
    private readonly IStreams streams;
    
    private readonly IServiceProvider serviceProvider;

    public Processor(IStreams streams, IServiceProvider serviceProvider)
    {
        this.streams = streams;
        this.serviceProvider = serviceProvider;
    }

    public async Task<IProcessor.Result> ProcessNextEvent(CancellationToken cancellationToken)
    {
        var eventToProcess = await this.streams.GetNextUnprocessedEvent(this.GetType().AssemblyQualifiedName!, cancellationToken);
        if (eventToProcess is null)
        {
            return IProcessor.Result.NothingToProcess;
        }
        
        var handler = this.serviceProvider.GetKeyedService<IEventHandler>(eventToProcess.GetType().AssemblyQualifiedName!);
        if (handler is null)
        {
            return IProcessor.Result.ProcessingError;
        }

        await handler.Handle(eventToProcess, cancellationToken);
        await this.streams.MarkEventAsProcessed(eventToProcess, this.GetType().AssemblyQualifiedName!, cancellationToken);
        return IProcessor.Result.EventProcessed;
    }
}