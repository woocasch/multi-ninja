namespace MultiNinja.Backend.WebApi.Endpoints.AuthModels;

public sealed class CreateTokenInput
{
    public CreateTokenInput(string userName, string password)
    {
        this.UserName = userName;
        this.Password = password;
    }

    public string UserName { get; }
    
    public string Password { get; }
}