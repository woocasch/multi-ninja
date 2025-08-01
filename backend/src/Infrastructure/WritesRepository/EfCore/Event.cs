namespace MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;

public class Event
{
    // public ulong Id { get; set; }

    public Guid EventId { get; set; }

    public Guid StreamId { get; set; }

    public Stream Stream { get; set; } = null!;

    public string EntityType { get; set; } = string.Empty;
    
    public DateTime EventTime { get; set; }

    public string TypeName { get; set; } = string.Empty;
    
    public string SerializedEvent { get; set; } = string.Empty;
    
    public ulong Version { get; set; }

    public ulong Position { get; set; }
}