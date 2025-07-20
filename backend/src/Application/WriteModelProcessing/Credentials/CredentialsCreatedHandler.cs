using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Credentials;
using MultiNinja.Backend.Domain.Credentials;

namespace MultiNinja.Backend.Application.WriteModelProcessing.Credentials;

public class CredentialsCreatedHandler : EventHandlerBase<CredentialsCreated>
{
    private readonly ICredentials credentials;

    public CredentialsCreatedHandler(ICredentials credentials)
    {
        this.credentials = credentials;
    }

    protected override async Task Handle(CredentialsCreated entityEvent, CancellationToken cancellationToken)
    {
        var parameters = new CreateCredentialsParameters(
            entityEvent.CredentialsId,
            entityEvent.UserId,
            entityEvent.Email,
            entityEvent.PasswordHash);
        await this.credentials.Create(parameters, cancellationToken);
    }
}