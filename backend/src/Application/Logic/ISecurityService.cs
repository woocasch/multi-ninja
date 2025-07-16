using MultiNinja.Backend.Application.Logic.Security;

namespace MultiNinja.Backend.Application.Logic;

public interface ISecurityService
{
    Task<OneOf<CreateCredentialsResponse, CreateCredentialsError>> CreateCredentials(
        CreateCredentialsRequest request,
        CancellationToken cancellationToken);
}