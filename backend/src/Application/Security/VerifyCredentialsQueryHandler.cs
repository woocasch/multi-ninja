using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Application.ReadsRepository.Credentials;

namespace MultiNinja.Backend.Application.Security;

public sealed class VerifyCredentialsQueryHandler : QueryHandlerBase<VerifyCredentialsQuery, VerifyCredentialsResult>
{
    private readonly ICredentials credentials;

    public VerifyCredentialsQueryHandler(ICredentials credentials)
    {
        this.credentials = credentials;
    }

    protected override async Task<VerifyCredentialsResult> Fetch(VerifyCredentialsQuery query, CancellationToken cancellationToken)
    {
        var checkCredentialsParameters = new CheckCredentialsParameters(query.Email, query.Password);
        var checkCredentialsResult = await this.credentials.CheckCredentials(checkCredentialsParameters, cancellationToken);
        if (checkCredentialsResult is null)
        {
            return new VerifyCredentialsResult(null);
        }

        return new VerifyCredentialsResult(checkCredentialsResult.Id);
    }
}