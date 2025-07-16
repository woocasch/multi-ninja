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

    public async Task<OneOf<CreateCredentialsResponse, CreateCredentialsError>> CreateCredentials(CreateCredentialsRequest request, CancellationToken cancellationToken)
    {
        var existingCredentials = await this.credentials.SearchByEmail(
            new(request.Email),
            cancellationToken);
        if (existingCredentials is not null)
        {
            return CreateCredentialsError.EmailTaken;
        }
        
        var id = Guid.NewGuid();
        var parameters = new CreateCredentialsParameters(
            id,
            request.Email,
            request.Password);
        var result = await this.credentials.Create(parameters, cancellationToken);
        return !result ? CreateCredentialsError.UnknownError : new CreateCredentialsResponse(id);
    }
}