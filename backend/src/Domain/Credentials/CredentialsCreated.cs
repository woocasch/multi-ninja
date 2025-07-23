using System.Text.Json.Serialization;

namespace MultiNinja.Backend.Domain.Credentials;

public sealed class CredentialsCreated : EntityEvent
{   
    [JsonConstructor]
    private CredentialsCreated(
        Guid streamId,
        Guid credentialsId,
        Guid userId,
        string email,
        string passwordHash,
        DateTime storageDate,
        ulong version)
        : base(streamId, EntityType.Credentials, storageDate, version)
    {
        this.CredentialsId = credentialsId;
        this.UserId = userId;
        this.Email = email;
        this.PasswordHash = passwordHash;
    }

    public Guid CredentialsId { get; }

    public string Email { get; }
    
    public string PasswordHash { get; }
    
    public Guid UserId { get; }

    public static CredentialsCreated Create(
        Guid streamId,
        Guid credentialsId,
        Guid userId,
        string email,
        string passwordHash)
    {
        if (streamId == Guid.Empty)
        {
            throw new ArgumentException(
                CredentialsCreatedResources.StreamId_Required,
                nameof(streamId));
        }

        if (credentialsId == Guid.Empty)
        {
            throw new ArgumentException(
                CredentialsCreatedResources.CredentialsId_Required,
                nameof(credentialsId));
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException(
                CredentialsCreatedResources.UserId_Required,
                nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(
                CredentialsCreatedResources.Email_Required,
                nameof(email));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException(
                CredentialsCreatedResources.PasswordHash_Required,
                nameof(passwordHash));
        }

        return new CredentialsCreated(
            streamId,
            credentialsId,
            userId,
            email,
            passwordHash,
            DateTime.UtcNow,
            0);
    }
}