namespace MultiNinja.Backend.Domain.Credentials;

public sealed class CredentialsEntity : Entity
{
    private CredentialsEntity(Guid streamId, Guid entityId)
        : base(streamId, EntityType.Credentials, entityId)
    {
    }

    public Guid CredentialsId => this.EntityId;

    public Guid UserId { get; private set; }

    public string UserName { get; private set; } = string.Empty;

    public string PasswordSalt { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public static CredentialsEntity Create(
        Guid credentialsId,
        Guid userId,
        string userName,
        string passwordSalt,
        string passwordHash)
    {
        var result = new CredentialsEntity(
            Guid.NewGuid(),
            userId);
        var userCreatedEvent = CredentialsCreated.Create(
            result.StreamId,
            credentialsId,
            userId,
            userName,
            passwordSalt,
            passwordHash);
        result.Apply(userCreatedEvent);
        return result;
    }

    protected override void When(EntityEvent entityEvent)
    {
        switch (entityEvent)
        {
            case CredentialsCreated credentialsCreated:
                this.When(credentialsCreated);
                break;
            default:
                throw new InvalidOperationException(
                    $"Missing handler for '{entityEvent.GetType().FullName}'.");
        }
    }

    private void When(CredentialsCreated credentialsCreated)
    {
        this.EntityId = credentialsCreated.CredentialsId;
        this.UserId = credentialsCreated.UserId;
        this.UserName = credentialsCreated.UserName;
        this.PasswordSalt = credentialsCreated.PasswordSalt;
        this.PasswordHash = credentialsCreated.PasswordHash;
    }
}