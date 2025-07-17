namespace MultiNinja.Backend.Application;

public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TResult>
    where TResult : class
    where TQuery : IQuery<TResult>
{
    public async Task<TResult> Fetch(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        if (query is not TQuery cast)
        {
            throw new InvalidOperationException(
                $"Can only handle instances of '{typeof(TQuery).FullName}'.");
        }
        
        return await this.Fetch(cast, cancellationToken);
    }
    
    protected abstract Task<TResult> Fetch(TQuery query, CancellationToken cancellationToken);
}