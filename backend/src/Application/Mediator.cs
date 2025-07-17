using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application;

public class Mediator : IMediator
{
    private readonly IServiceProvider serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<CommandExecutionResult> Execute<TCommand>(
        TCommand command,
        CancellationToken cancellationToken)
        where TCommand : ICommand
    {
        var handler =
            this.serviceProvider.GetKeyedService<ICommandHandler>(typeof(TCommand).AssemblyQualifiedName);
        if (handler is null)
        {
            throw new InvalidOperationException(
                $"Command handler for '{typeof(TCommand).AssemblyQualifiedName}' could not be found.");
        }

        return await handler.Execute(command, cancellationToken);
    }

    public async Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : class
    {
        var handler =
            this.serviceProvider.GetKeyedService<IQueryHandler<TResult>>(query.GetType().AssemblyQualifiedName);
        if (handler is null)
        {
            throw new InvalidOperationException(
                $"Query handler for '{query.GetType().AssemblyQualifiedName}' could not be found.");
        }

        return await handler.Fetch(query, cancellationToken);
    }
}