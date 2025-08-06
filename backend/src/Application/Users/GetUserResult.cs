namespace MultiNinja.Backend.Application.Users;

public sealed class GetUserResult
{
    public GetUserResult(UserData? user)
    {
        this.User = user;
    }

    public UserData? User { get; }

    public record UserData(Guid Id, string DisplayName);
}