using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.WritesRepository;

namespace MultiNinja.Backend.Application.WriteModelProcessing;

public sealed class Processor : IProcessor
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
        
        var handler = this.serviceProvider.GetKeyedService<IEventHandler>(eventToProcess.EventData.GetType().AssemblyQualifiedName!);
        if (handler is null)
        {
            return IProcessor.Result.ProcessingError;
        }

        eventToProcess.EventData.Version = eventToProcess.Version;
        await handler.Handle(eventToProcess.EventData, cancellationToken);
        await this.streams.MarkEventAsProcessed(eventToProcess, this.GetType().AssemblyQualifiedName!, cancellationToken);
        return IProcessor.Result.EventProcessed;
    }
}