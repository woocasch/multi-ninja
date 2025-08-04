using MultiNinja.Backend.Application.Cryptography;
using MultiNinja.Backend.Application.EventStreams;
using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Domain.Credentials;

namespace MultiNinja.Backend.Application.Security;

public sealed class CreateCredentialsCommandHandler : CommandHandlerBase<CreateCredentialsCommand>
{
    private readonly ICredentials credentialsRepository;
    
    private readonly IPasswordsCryptography passwordsCryptography;

    private readonly IEventStreamsService eventStreams;

    public CreateCredentialsCommandHandler(
        ICredentials credentialsRepository,
        IEventStreamsService eventStreams,
        IPasswordsCryptography passwordsCryptography)
    {
        this.credentialsRepository = credentialsRepository;
        this.eventStreams = eventStreams;
        this.passwordsCryptography = passwordsCryptography;
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

        var passwordData = await this.PreparePassword(command.Password, cancellationToken);
        var credentials = CredentialsEntity.Create(
            command.Id,
            command.UserId,
            command.UserName,
            passwordData.Salt,
            passwordData.Hash);
        await this.eventStreams.Create(credentials,  cancellationToken);
        return CommandExecutionResult.Succeeded();
    }

    private async Task<(string Salt, string Hash)> PreparePassword(string password, CancellationToken cancellationToken)
    {
        var salt = await this.passwordsCryptography.GeneratePasswordSalt(cancellationToken);
        var hash = await this.passwordsCryptography.GeneratePasswordHash(password, salt, cancellationToken);
        return (salt, hash);
    }
}