namespace MultiNinja.Backend.WebApi.Orchestration.Accounts;

public sealed class CreateTokenResponse
{
    public CreateTokenResponse(
        string userName,
        string displayName,
        string token)
    {
        this.UserName = userName;
        this.DisplayName = displayName;
        this.Token = token;
    }

    public string UserName { get; }
    
    public string DisplayName { get; }

    public string Token { get; }
}