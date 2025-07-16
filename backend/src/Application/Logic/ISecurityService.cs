using MultiNinja.Backend.Application.Logic.Security;

namespace MultiNinja.Backend.Application.Logic;

public interface ISecurityService
{
    Task<CreateCredentialsResponse> CreateCredentials(
        CreateCredentialsRequest request,
        CancellationToken cancellationToken);
}