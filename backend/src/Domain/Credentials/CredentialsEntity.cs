namespace MultiNinja.Backend.Domain.Credentials;

public sealed class CredentialsEntity : Entity
{
    private CredentialsEntity(Guid streamId, Guid entityId)
        : base(streamId, EntityType.Credentials, entityId)
    {
    }

    public Guid CredentialsId => this.EntityId;

    public Guid UserId { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string HashedPassword { get; private set; } = string.Empty;

    public static CredentialsEntity Create(
        Guid credentialsId,
        Guid userId,
        string email,
        string passwordHash)
    {
        var result = new CredentialsEntity(
            Guid.NewGuid(),
            userId);
        var userCreatedEvent = CredentialsCreated.Create(
            result.StreamId,
            credentialsId,
            userId,
            email,
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
        this.Email = credentialsCreated.Email;
        this.HashedPassword = credentialsCreated.PasswordHash;
    }
}