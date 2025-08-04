using System.Text.Json.Serialization;

namespace MultiNinja.Backend.Domain.Credentials;

public sealed class CredentialsCreated : EntityEvent
{   
    [JsonConstructor]
    private CredentialsCreated(
        Guid streamId,
        Guid credentialsId,
        Guid userId,
        string userName,
        string passwordHash,
        DateTime storageDate)
        : base(streamId, EntityType.Credentials, storageDate)
    {
        this.CredentialsId = credentialsId;
        this.UserId = userId;
        this.UserName = userName;
        this.PasswordHash = passwordHash;
    }

    public Guid CredentialsId { get; }

    public string UserName { get; }
    
    public string PasswordHash { get; }
    
    public Guid UserId { get; }

    public static CredentialsCreated Create(
        Guid streamId,
        Guid credentialsId,
        Guid userId,
        string userName,
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

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException(
                CredentialsCreatedResources.UserName_Required,
                nameof(userName));
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
            userName,
            passwordHash,
            DateTime.UtcNow);
    }
}