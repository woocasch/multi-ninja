using MultiNinja.Backend.Application.WritesRepository.Streams;
using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.WritesRepository;

public interface IStreams
{
    Task CreateStream(CreateStreamParameters parameters, CancellationToken cancellationToken);
    
    Task<ulong> AddEvent(AddEventParameters parameters, CancellationToken cancellationToken);
    
    Task<EntityEventEnvelope?> GetNextUnprocessedEvent(string processorName, CancellationToken cancellationToken);
    
    Task MarkEventAsProcessed(EntityEventEnvelope entityEvent, string processorName, CancellationToken cancellationToken);
}