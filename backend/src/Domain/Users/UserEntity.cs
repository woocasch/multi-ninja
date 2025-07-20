namespace MultiNinja.Backend.Domain.Users;

public sealed class UserEntity : Entity
{
    public Guid UserId => this.EntityId;

    public string DisplayName { get; private set; } = string.Empty;

    private UserEntity(Guid streamId, Guid entityId)
        : base(streamId, EntityType.User, entityId)
    {
    }

    public static UserEntity Create(
        Guid userId,
        string displayName)
    {
        var result = new UserEntity(
            Guid.NewGuid(),
            userId);
        var userCreatedEvent = UserCreated.Create(
            result.StreamId,
            userId,
            DateTime.UtcNow,
            displayName);
        result.Apply(userCreatedEvent);
        return result;
    }

    protected override void When(EntityEvent entityEvent)
    {
        switch (entityEvent)
        {
            case UserCreated userCreated:
                this.When(userCreated);
                break;
            default:
                throw new InvalidOperationException(
                    $"Missing handler for '{entityEvent.GetType().FullName}'.");
        }
    }

    private void When(UserCreated userCreated)
    {
        this.EntityId = userCreated.UserId;
        this.DisplayName = userCreated.DisplayName;
    }
}