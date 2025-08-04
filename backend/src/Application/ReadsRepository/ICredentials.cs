using MultiNinja.Backend.Application.ReadsRepository.Credentials;

namespace MultiNinja.Backend.Application.ReadsRepository;

public interface ICredentials
{
    Task<bool> Create(CreateCredentialsParameters parameters, CancellationToken cancellationToken);
    
    Task<CredentialsRecord?>CheckCredentials(CheckCredentialsParameters parameters, CancellationToken cancellationToken);
    
    Task<CredentialsRecord?> SearchByUserName(SearchByEmailParameters parameters, CancellationToken cancellationToken);
}