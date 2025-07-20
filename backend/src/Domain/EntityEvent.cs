namespace MultiNinja.Backend.Domain;

public abstract class EntityEvent
{
    protected EntityEvent(
        Guid streamId,
        EntityType entityType,
        DateTime storageDate,
        ulong version)
    {
        this.StreamId = streamId;
        this.EntityType = entityType;
        this.StorageDate = storageDate;
        this.Version = version;
    }

    public Guid StreamId { get; }

    public EntityType EntityType { get; }
    
    public DateTime StorageDate { get; }

    public ulong Version { get; }
}