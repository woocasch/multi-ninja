using MultiNinja.Backend.Application.WritesRepository;
using MultiNinja.Backend.Application.WritesRepository.Streams;
using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.EventStreams;

public sealed class EventStreamService : IEventStreamsService
{
    private readonly IStreams streams;

    public EventStreamService(IStreams streams)
    {
        this.streams = streams;
    }

    public async Task Create<TEntity>(TEntity newEntity, CancellationToken cancellationToken) where TEntity : Entity
    {
        var createStreamParameters = new CreateStreamParameters(
            newEntity.StreamId,
            newEntity.EntityType,
            newEntity.EntityId);
        await this.streams.CreateStream(createStreamParameters, cancellationToken);
        var uncommitedEvents = newEntity.GetUncommittedEvents();
        var currentVersion = newEntity.Version;
        foreach (var uncommitedEvent in uncommitedEvents)
        {
            try
            {
                var addEventParameters = new AddEventParameters(
                    uncommitedEvent,
                    uncommitedEvent.StorageDate,
                    currentVersion);
                currentVersion = await this.streams.AddEvent(addEventParameters, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

    public async Task Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : Entity
    {
        var uncommitedEvents = entity.GetUncommittedEvents();
        var currentVersion = entity.Version;
        foreach (var uncommitedEvent in uncommitedEvents)
        {
            var addEventParameters = new AddEventParameters(
                uncommitedEvent,
                uncommitedEvent.StorageDate,
                currentVersion);
            currentVersion = await this.streams.AddEvent(addEventParameters, cancellationToken);
        }
    }

    public async Task<TEntity?> Get<TEntity>(Guid id, CancellationToken cancellationToken)
        where TEntity : Entity, new()
    {
        await Task.Yield();
        return null;
    }

    public async Task<TEntity?> Get<TEntity>(EntityType entityType, Guid entityId, CancellationToken cancellationToken)
        where TEntity : Entity, new()
    {
        var stream = await this.streams.GetEntityStream(new(entityId, entityType), cancellationToken);
        if (stream is null)
        {
            return null;
        }

        var events = await this.streams.FetchStreamEvents(stream.StreamId, cancellationToken);
        var result = new TEntity();
        foreach (var @event in events)
        {
            result.Apply(@event.EnvelopedEvent.EventData);
        }

        return result;
    }
}