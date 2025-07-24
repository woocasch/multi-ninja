namespace MultiNinja.Backend.Application.WriteModelProcessing;

public interface IProcessor
{
    Task<Result> ProcessNextEvent(CancellationToken cancellationToken);
    
    public enum Result
    {
        None = 0,
        EventProcessed = 1,
        ProcessingError = 2,
        NothingToProcess = 3,
    }
}