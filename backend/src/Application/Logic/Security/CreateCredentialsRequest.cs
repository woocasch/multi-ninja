namespace MultiNinja.Backend.Application.Logic.Security;

public class CreateCredentialsRequest
{
    public CreateCredentialsRequest(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }

    public string Email { get; }

    public string Password { get; }
}