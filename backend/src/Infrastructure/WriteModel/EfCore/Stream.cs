namespace MultiNinja.Backend.Infrastructure.WriteModel.EfCore;

public class Stream
{
    // public ulong Id { get; set; }
    
    public Guid StreamId { get; set; }

    public string EntityType { get; set; } = string.Empty;
    
    public Guid EntityId { get; set; }

    public virtual ICollection<Event> Events { get; set; } = [];
}