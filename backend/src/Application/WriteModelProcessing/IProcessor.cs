namespace MultiNinja.Backend.Application.WriteModelProcessing;

public interface IProcessor
{
    Task<Result> ProcessNextEvent(CancellationToken cancellationToken);
    
    public enum Result
    {
        EventProcessed,
        ProcessingError,
        NothingToProcess,
    }
}