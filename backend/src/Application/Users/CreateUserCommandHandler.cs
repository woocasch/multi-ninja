using MultiNinja.Backend.Application.EventStreams;
using MultiNinja.Backend.Domain.Users;

namespace MultiNinja.Backend.Application.Users;

public sealed class CreateUserCommandHandler : CommandHandlerBase<CreateUserCommand>
{
    private readonly IEventStreamsService streamsService;

    public CreateUserCommandHandler(IEventStreamsService streamsService)
    {
        this.streamsService = streamsService;
    }

    protected override async Task<CommandExecutionResult> Execute(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = UserEntity.Create(command.Id, command.DisplayName);
        await this.streamsService.Create(user, cancellationToken);
        return CommandExecutionResult.Succeeded();
    }
}