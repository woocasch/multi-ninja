namespace MultiNinja.Backend.Domain.Users;

public sealed class UserCreated : EntityEvent
{
    private UserCreated(
        Guid streamId,
        Guid userId,
        DateTime storageDate,
        string displayName,
        ulong version)
        : base(streamId, EntityType.User, storageDate, version)
    {
        this.UserId = userId;
        this.DisplayName = displayName;
    }

    public Guid UserId { get; }

    public string DisplayName { get; }

    public static UserCreated Create(
        Guid streamId,
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
        
        return new UserCreated(
            streamId,
            userId,
            storageDate,
            displayName,
            0);
    }
}