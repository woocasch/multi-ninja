using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.WritesRepository.Streams;

public class StreamDto
{
    public StreamDto(Guid streamId, Guid entityId, EntityType entityType)
    {
        this.StreamId = streamId;
        this.EntityId = entityId;
        this.EntityType = entityType;
    }

    public Guid StreamId { get; }
    
    public Guid EntityId { get; }
    
    public EntityType EntityType { get; }
}