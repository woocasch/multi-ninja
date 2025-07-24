using MultiNinja.Backend.Application.EventStreams;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Credentials;
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
        var existingCredentials = await this.credentialsRepository.SearchByEmail(
            new(command.Email),
            cancellationToken);
        if (existingCredentials is not null)
        {
            return CommandExecutionResult.Failed(CreateCredentialsError.EmailTaken);
        }
        
        var credentials = CredentialsEntity.Create(
            command.Id,
            command.UserId,
            command.Email,
            command.Password);
        await this.eventStreams.Create(credentials,  cancellationToken);
        return CommandExecutionResult.Succeeded();
    }
}