namespace MultiNinja.Backend.WebApi.Endpoints.AuthModels;

public class CreateTokenInput
{
    public CreateTokenInput(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }

    public string Email { get; }
    
    public string Password { get; }
}