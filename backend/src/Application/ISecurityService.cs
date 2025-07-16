using MultiNinja.Backend.Application.Security;

namespace MultiNinja.Backend.Application;

public interface ISecurityService
{
    Task<CreateCredentialsResponse> CreateCredentials(
        CreateCredentialsRequest request,
        CancellationToken cancellationToken);
}