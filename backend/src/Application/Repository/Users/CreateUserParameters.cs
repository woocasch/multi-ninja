namespace MultiNinja.Backend.Application.Repository.Users;

public class CreateUserParameters
{
    public CreateUserParameters(Guid userId, string displayName)
    {
        this.UserId = userId;
        this.DisplayName = displayName;
    }

    public Guid UserId { get; }

    public string DisplayName { get; }
}