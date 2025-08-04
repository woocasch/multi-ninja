using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Application.ReadsRepository.Credentials;
using MultiNinja.Backend.Domain.Credentials;

namespace MultiNinja.Backend.Application.WriteModelProcessing.Credentials;

public sealed class CredentialsCreatedHandler : EventHandlerBase<CredentialsCreated>
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
            entityEvent.UserName);
        await this.credentials.Create(parameters, cancellationToken);
    }
}