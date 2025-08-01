namespace MultiNinja.Backend.Infrastructure.WriteModel.EfCore;

public class ProcessorsProgress
{
    public string ProcessorName { get; set; } = string.Empty;
    
    public ulong LastProcessedPosition { get; set; }
}