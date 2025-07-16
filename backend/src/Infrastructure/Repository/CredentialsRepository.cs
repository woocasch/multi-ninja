using System.Collections.ObjectModel;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Credentials;

namespace MultiNinja.Backend.Infrastructure.Repository;

public class CredentialsRepository : ICredentials
{
    private readonly Collection<CredentialsDataRecord> credentials = new();
    
    public async Task<bool> Create(CreateCredentialsParameters parameters, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var dataRecord = new CredentialsDataRecord()
        {
            Id = parameters.Id,
            Email = parameters.Email,
            Password = parameters.Password,
        };
        
        this.credentials.Add(dataRecord);
        return true;
    }

    public async Task<CredentialsRecord?> SearchByEmail(SearchByEmailParameters parameters, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var found = this.credentials.Where(c => c.Email == parameters.Email).ToList();
        if (found.Count == 0)
        {
            return null;
        }

        if (found.Count == 1)
        {
            var item = found[0];
            return new(item.Id, item.Email);
        }
        
        throw new InvalidOperationException();
    }
}