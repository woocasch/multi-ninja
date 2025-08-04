using MultiNinja.Backend.Application.Cryptography;
using MultiNinja.Backend.Application.EventStreams;
using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Application.ReadsRepository.Credentials;
using MultiNinja.Backend.Domain;
using MultiNinja.Backend.Domain.Credentials;

namespace MultiNinja.Backend.Application.Security;

public sealed class VerifyCredentialsQueryHandler : QueryHandlerBase<VerifyCredentialsQuery, VerifyCredentialsResult>
{
    private readonly IEventStreamsService streams;

    private readonly ICredentials credentials;
    
    private readonly IPasswordsCryptography passwordsCryptography;

    public VerifyCredentialsQueryHandler(
        IEventStreamsService streams,
        ICredentials credentials,
        IPasswordsCryptography passwordsCryptography)
    {
        this.streams = streams;
        this.credentials = credentials;
        this.passwordsCryptography = passwordsCryptography;
    }

    protected override async Task<VerifyCredentialsResult> Fetch(VerifyCredentialsQuery query, CancellationToken cancellationToken)
    {
        var checkCredentialsResult = await this.CheckCredentials(query.UserName, query.Password, cancellationToken);
        if (checkCredentialsResult is null)
        {
            return new VerifyCredentialsResult(null);
        }

        return new VerifyCredentialsResult(checkCredentialsResult.CredentialsId);
    }

    private async Task<CredentialsEntity?> CheckCredentials(
        string userName,
        string password,
        CancellationToken cancellationToken)
    {
        var credentialsInfo = await this.credentials.SearchByUserName(new(userName), cancellationToken);
        if (credentialsInfo is null)
        {
            return null;
        }

        var entity = await this.streams.Get<CredentialsEntity>(
            EntityType.Credentials,
            credentialsInfo.Id,
            cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var calculatedHash = await this.HashPassword(password, entity.PasswordSalt, cancellationToken);
        if (calculatedHash != entity.PasswordHash)
        {
            return null;
        }

        return entity;
    }

    private async Task<string> HashPassword(string password, string salt, CancellationToken cancellationToken)
    {
        var hash = await this.passwordsCryptography.GeneratePasswordHash(password, salt, cancellationToken);
        return hash;
    }
}