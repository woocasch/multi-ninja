namespace MultiNinja.Backend.Application.Orchestration.Accounts;

public class CreateTokenRequest
{
    public CreateTokenRequest(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }

    public string Email { get; }
    
    public string Password { get; }
}