using MultiNinja.Backend.Application.Repository.Streams;

namespace MultiNinja.Backend.Application.Repository;

public interface IStreams
{
    Task CreateStream(CreateStreamParameters parameters, CancellationToken cancellationToken);
    
    Task<ulong> AddEvent(AddEventParameters parameters, CancellationToken cancellationToken);
}