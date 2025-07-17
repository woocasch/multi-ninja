namespace MultiNinja.Backend.Application;

public interface IQueryHandler<TResult>
    where TResult : class
{
    Task<TResult> Fetch(IQuery<TResult> query, CancellationToken cancellationToken);
}