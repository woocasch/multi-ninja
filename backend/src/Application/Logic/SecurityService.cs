using MultiNinja.Backend.Application.Logic.Security;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Credentials;

namespace MultiNinja.Backend.Application.Logic;

public class SecurityService : ISecurityService
{
    private readonly ICredentials credentials;

    public SecurityService(ICredentials credentials)
    {
        this.credentials = credentials;
    }

    public async Task<VerifyCredentialsResponse?> VerifyCredentials(VerifyCredentialsRequest request, CancellationToken cancellationToken)
    {
        var checkCredentialsParameters = new CheckCredentialsParameters(request.Email, request.Password);
        var checkCredentialsResult = await this.credentials.CheckCredentials(checkCredentialsParameters, cancellationToken);
        if (checkCredentialsResult is null)
        {
            return null;
        }

        return new VerifyCredentialsResponse(checkCredentialsResult.Id);
    }
}