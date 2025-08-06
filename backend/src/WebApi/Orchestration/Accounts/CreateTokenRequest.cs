namespace MultiNinja.Backend.WebApi.Orchestration.Accounts;

public sealed class CreateTokenRequest
{
    public CreateTokenRequest(string userName, string password)
    {
        this.UserName = userName;
        this.Password = password;
    }

    public string UserName { get; }
    
    public string Password { get; }
}