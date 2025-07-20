using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.Repository.Streams;

public sealed class AddEventParameters
{
    public AddEventParameters(EntityEvent eventData, DateTime storageDate, ulong entityVersion)
    {
        this.EventData = eventData;
        this.StorageDate = storageDate;
        this.EntityVersion = entityVersion;
    }

    public Guid StreamId => this.EventData.StreamId;

    public EntityType EntityType => this.EventData.EntityType;

    public DateTime StorageDate { get; }

    public ulong EntityVersion { get; }

    public EntityEvent EventData { get; }
}