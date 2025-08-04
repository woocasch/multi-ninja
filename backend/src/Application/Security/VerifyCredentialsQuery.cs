namespace MultiNinja.Backend.Application.Security;

public sealed class VerifyCredentialsQuery : IQuery<VerifyCredentialsResult>
{
    public VerifyCredentialsQuery(string userName, string password)
    {
        this.UserName = userName;
        this.Password = password;
    }

    public string UserName { get; }

    public string Password { get; }
}