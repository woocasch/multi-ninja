using System.Collections.ObjectModel;

namespace MultiNinja.Backend.Domain;

public abstract class Entity
{
    private readonly Collection<EntityEvent> events = [];

    private ulong? version;

    protected Entity(Guid streamId, EntityType entityType, Guid entityId)
    {
        this.StreamId = streamId;
        this.EntityType = entityType;
        this.EntityId = entityId;
    }

    public Guid StreamId { get; }

    public EntityType EntityType { get; }

    public Guid EntityId { get; protected set; }

    public ulong Version => this.version ?? 0;

    public ReadOnlyCollection<EntityEvent> Events => new(this.events);

    public void Apply<T>(T entityEvent)
        where T : EntityEvent
    {
        this.When(entityEvent);
        this.events.Add(entityEvent);
        this.IncreaseVersion();
    }

    public IEnumerable<EntityEvent> GetUncommittedEvents()
    {
        return this.events.Where(e => e.Version == 0);
    }

    protected abstract void When(EntityEvent entityEvent);

    private void IncreaseVersion()
    {
        if (!this.version.HasValue)
        {
            this.version = 0;
        }

        this.version++;
    }
}