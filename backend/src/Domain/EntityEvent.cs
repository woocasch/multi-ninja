namespace MultiNinja.Backend.Domain;

public abstract class EntityEvent
{
    protected EntityEvent(
        Guid streamId,
        EntityType entityType,
        DateTime storageDate)
    {
        this.StreamId = streamId;
        this.EntityType = entityType;
        this.StorageDate = storageDate;
    }

    public Guid StreamId { get; }

    public EntityType EntityType { get; }
    
    public DateTime StorageDate { get; }

    internal ulong Version { get; set; }
}