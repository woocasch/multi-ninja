namespace MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;

public sealed class ProcessorsProgress
{
    public string ProcessorName { get; set; } = string.Empty;
    
    public ulong LastProcessedPosition { get; set; }
}