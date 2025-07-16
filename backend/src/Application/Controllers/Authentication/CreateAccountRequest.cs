namespace MultiNinja.Backend.Application.Controllers.Authentication;

public class CreateAccountRequest
{
    public CreateAccountRequest(string email, string password, string displayName)
    {
        this.Email = email;
        this.Password = password;
        this.DisplayName = displayName;
    }

    public string Email { get; }

    public string Password { get; }

    public string DisplayName { get; }
}