using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.Repository.Streams;

public sealed class CreateStreamParameters
{
    public CreateStreamParameters(Guid streamId, EntityType entityType, Guid entityId)
    {
        this.StreamId = streamId;
        this.EntityType = entityType;
        this.EntityId = entityId;
    }

    public Guid StreamId { get; }

    public EntityType EntityType { get; }
    
    public Guid EntityId { get; }
}