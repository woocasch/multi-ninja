namespace MultiNinja.Backend.Application.Security;

public sealed class VerifyCredentialsQuery : IQuery<VerifyCredentialsResult>
{
    public VerifyCredentialsQuery(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }

    public string Email { get; }

    public string Password { get; }
}