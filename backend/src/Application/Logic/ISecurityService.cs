using MultiNinja.Backend.Application.Logic.Security;

namespace MultiNinja.Backend.Application.Logic;

public interface ISecurityService
{
    Task<VerifyCredentialsResponse?> VerifyCredentials(
        VerifyCredentialsRequest request,
        CancellationToken cancellationToken);
}