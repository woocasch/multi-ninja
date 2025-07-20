namespace MultiNinja.Backend.Domain.Users;

public sealed class UserEntity : Entity
{
    public Guid UserId => this.EntityId;

    public string DisplayName { get; private set; } = string.Empty;

    private UserEntity(Guid streamId, EntityType entityType, Guid entityId)
        : base(streamId, entityType, entityId)
    {
    }

    public static UserEntity Create(
        Guid userId,
        string displayName)
    {
        var result = new UserEntity(
            Guid.NewGuid(),
            EntityType.User,
            userId);
        var userCreatedEvent = UserCreated.Create(
            result.StreamId,
            result.EntityType,
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
        }
    }

    private void When(UserCreated userCreated)
    {
        this.EntityId = userCreated.UserId;
        this.DisplayName = userCreated.DisplayName;
    }
}