using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Streams;
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
            var addEventParameters = new AddEventParameters(
                uncommitedEvent,
                uncommitedEvent.StorageDate,
                currentVersion);
            currentVersion = await this.streams.AddEvent(addEventParameters, cancellationToken);
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

    public Task<TEntity> Get<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : Entity
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> Get<TEntity>(EntityType entityType, Guid entityId, CancellationToken cancellationToken)
        where TEntity : Entity
    {
        throw new NotImplementedException();
    }
}