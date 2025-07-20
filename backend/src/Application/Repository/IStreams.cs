using MultiNinja.Backend.Application.Repository.Streams;
using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.Repository;

public interface IStreams
{
    Task CreateStream(CreateStreamParameters parameters, CancellationToken cancellationToken);
    
    Task<ulong> AddEvent(AddEventParameters parameters, CancellationToken cancellationToken);
    
    Task<EntityEvent?> GetNextUnprocessedEvent(string processorName, CancellationToken cancellationToken);
    
    Task MarkEventAsProcessed(EntityEvent entityEvent, string processorName, CancellationToken cancellationToken);
}