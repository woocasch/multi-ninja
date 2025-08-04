using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.EventStreams;

public interface IEventStreamsService
{
    Task Create<TEntity>(TEntity newEntity, CancellationToken cancellationToken)
        where TEntity : Entity;

    Task Update<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : Entity;

    Task<TEntity?> Get<TEntity>(Guid id, CancellationToken cancellationToken)
        where TEntity : Entity;

    Task<TEntity?> Get<TEntity>(EntityType entityType, Guid entityId, CancellationToken cancellationToken)
        where TEntity : Entity;
}