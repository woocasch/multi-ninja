namespace MultiNinja.Backend.Domain;

public sealed class EntityEventEnvelope
{
    public EntityEventEnvelope(
        Guid streamId,
        EntityType entityType,
        DateTime storageDate,
        EntityEvent eventData,
        ulong version)
    {
        this.StreamId = streamId;
        this.EntityType = entityType;
        this.StorageDate = storageDate;
        this.EventData = eventData;
        this.Version = version;
    }

    public Guid StreamId { get; }

    public EntityType EntityType { get; }
    
    public DateTime StorageDate { get; }

    public EntityEvent EventData { get; }

    public ulong Version { get; }
}