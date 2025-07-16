namespace MultiNinja.Backend.Application.Logic.Security;

public class VerifyCredentialsRequest
{
    public VerifyCredentialsRequest(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }

    public string Email { get; }
    
    public string Password { get; }
}