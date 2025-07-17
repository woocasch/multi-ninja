namespace MultiNinja.Backend.Application;

public abstract class CommandHandlerBase<TCommand> : ICommandHandler
    where TCommand : ICommand
{
    public async Task<CommandExecutionResult> Execute(ICommand command, CancellationToken cancellationToken)
    {
        if (command is not TCommand cast)
        {
            throw new InvalidOperationException(
                $"Can only handle instances of '{typeof(TCommand).FullName}'.");
        }
        
        return await this.Execute(cast, cancellationToken);
    }

    protected abstract Task<CommandExecutionResult> Execute(
        TCommand command,
        CancellationToken cancellationToken);
}