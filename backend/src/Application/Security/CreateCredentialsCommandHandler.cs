using MultiNinja.Backend.Application.EventStreams;
using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Domain.Credentials;

namespace MultiNinja.Backend.Application.Security;

public sealed class CreateCredentialsCommandHandler : CommandHandlerBase<CreateCredentialsCommand>
{
    private readonly ICredentials credentialsRepository;

    private readonly IEventStreamsService eventStreams;

    public CreateCredentialsCommandHandler(
        ICredentials credentialsRepository,
        IEventStreamsService eventStreams)
    {
        this.credentialsRepository = credentialsRepository;
        this.eventStreams = eventStreams;
    }

    protected override async Task<CommandExecutionResult> Execute(
        CreateCredentialsCommand command,
        CancellationToken cancellationToken)
    {
        var existingCredentials = await this.credentialsRepository.SearchByUserName(
            new(command.UserName),
            cancellationToken);
        if (existingCredentials is not null)
        {
            return CommandExecutionResult.Failed(CreateCredentialsError.EmailTaken);
        }
        
        var credentials = CredentialsEntity.Create(
            command.Id,
            command.UserId,
            command.UserName,
            command.Password);
        await this.eventStreams.Create(credentials,  cancellationToken);
        return CommandExecutionResult.Succeeded();
    }
}