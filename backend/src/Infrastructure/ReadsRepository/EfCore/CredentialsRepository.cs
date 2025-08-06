using Microsoft.EntityFrameworkCore;
using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Application.ReadsRepository.Credentials;

namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public sealed class CredentialsRepository : ICredentials
{
    private readonly ReadsContext readsContext;

    public CredentialsRepository(ReadsContext readsContext)
    {
        this.readsContext = readsContext;
    }

    public async Task<bool> Create(CreateCredentialsParameters parameters, CancellationToken cancellationToken)
    {
        var payload = new Credentials()
        {
            Id = parameters.Id,
            UserId = parameters.UserId,
            UserName = parameters.UserName,
        };

        await this.readsContext.Credentials.AddAsync(payload, cancellationToken);
        return true;
    }

    public async Task<CredentialsRecord?> SearchByUserName(SearchByUserNameParameters parameters, CancellationToken cancellationToken)
    {
        var found = await this.readsContext.Credentials
            .Where(c => c.UserName == parameters.UserName)
            .ToListAsync(cancellationToken);
        if (found.Count == 0)
        {
            return null;
        }

        if (found.Count > 1)
        {
            throw new InvalidOperationException("Expected at most single record");
        }

        var result = new CredentialsRecord(found[0].Id, found[0].UserId, found[0].UserName);
        return result;
    }
}