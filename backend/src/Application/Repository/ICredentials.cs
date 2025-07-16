using MultiNinja.Backend.Application.Repository.Credentials;

namespace MultiNinja.Backend.Application.Repository;

public interface ICredentials
{
    Task<bool> Create(CreateCredentialsParameters parameters, CancellationToken cancellationToken);
    
    Task<CredentialsRecord?> SearchByEmail(SearchByEmailParameters parameters, CancellationToken cancellationToken);
}