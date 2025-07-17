namespace MultiNinja.Backend.Application;

public interface IMediator
{
    Task<CommandExecutionResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand;

    Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : class;
}