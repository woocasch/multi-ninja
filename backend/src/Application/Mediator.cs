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

        return await handler.Execute(command,  cancellationToken);
    }
}