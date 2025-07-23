namespace MultiNinja.Backend.Infrastructure.Repository.EfCore;

public class ProcessorsProgress
{
    public string ProcessorName { get; set; } = string.Empty;
    
    public ulong LastProcessedEventId { get; set; }
}