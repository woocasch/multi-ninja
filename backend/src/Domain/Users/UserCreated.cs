namespace MultiNinja.Backend.Domain.Users;

public sealed class UserCreated : EntityEvent
{
    private UserCreated(
        Guid streamId,
        EntityType entityType,
        Guid userId,
        DateTime storageDate,
        string displayName,
        ulong version)
        : base(streamId, entityType, storageDate, version)
    {
        this.UserId = userId;
        this.DisplayName = displayName;
    }

    public Guid UserId { get; }

    public string DisplayName { get; }

    public static UserCreated Create(
        Guid streamId,
        EntityType entityType,
        Guid userId,
        DateTime storageDate,
        string displayName)
    {
        if (streamId == Guid.Empty)
        {
            throw new ArgumentException(
                UserCreatedResources.StreamId_Required,
                nameof(streamId));
        }

        if (entityType is null)
        {
            throw new ArgumentNullException(
                UserCreatedResources.EntityType_Required,
                nameof(entityType));
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException(
                UserCreatedResources.UserId_Required,
                nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentNullException(
                UserCreatedResources.DisplayName_Required,
                nameof(displayName));
        }
        
        return new UserCreated(streamId, entityType, userId, storageDate, displayName, 0);
    }
}