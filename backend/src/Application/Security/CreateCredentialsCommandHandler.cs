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
        var saltBytes = await this.passwordsCryptography.GeneratePasswordSalt(cancellationToken);
        var passwordBytes = await this.passwordsCryptography.GeneratePasswordHash(password, saltBytes, cancellationToken);
        var salt = Convert.ToBase64String(saltBytes);
        var passwordHash = Convert.ToBase64String(passwordBytes);
        return (salt, passwordHash);
    }
}