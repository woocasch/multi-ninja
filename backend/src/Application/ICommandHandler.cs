namespace MultiNinja.Backend.Application;

public interface ICommandHandler
{
    Task<CommandExecutionResult> Execute(
        ICommand command,
        CancellationToken cancellationToken);
}