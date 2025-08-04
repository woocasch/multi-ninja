using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.WritesRepository.Streams;

public sealed class GetEntityStreamParameters
{
    public GetEntityStreamParameters(Guid entityId, EntityType entityType)
    {
        this.EntityId = entityId;
        this.EntityType = entityType;
    }

    public Guid EntityId { get; }
    
    public EntityType EntityType { get; }
}