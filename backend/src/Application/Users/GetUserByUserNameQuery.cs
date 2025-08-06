namespace MultiNinja.Backend.Application.Users;

public sealed class GetUserByUserNameQuery : IQuery<GetUserResult>
{
    public GetUserByUserNameQuery(string userName)
    {
        this.UserName = userName;
    }

    public string UserName { get; }
}