using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Credentials;

namespace MultiNinja.Backend.Application.Security;

public sealed class CreateCredentialsCommandHandler : CommandHandlerBase<CreateCredentialsCommand>
{
    private readonly ICredentials credentials;

    public CreateCredentialsCommandHandler(ICredentials credentials)
    {
        this.credentials = credentials;
    }

    protected override async Task<CommandExecutionResult> Execute(
        CreateCredentialsCommand command,
        CancellationToken cancellationToken)
    {
        var existingCredentials = await this.credentials.SearchByEmail(
            new(command.Email),
            cancellationToken);
        if (existingCredentials is not null)
        {
            return CommandExecutionResult.Failed(CreateCredentialsError.EmailTaken);
        }
        
        var parameters = new CreateCredentialsParameters(
            command.Id,
            command.Email,
            command.Password);
        var result = await this.credentials.Create(parameters, cancellationToken);
        return !result ? CommandExecutionResult.Failed(CreateCredentialsError.UnknownError) : CommandExecutionResult.Succeeded();
    }
}