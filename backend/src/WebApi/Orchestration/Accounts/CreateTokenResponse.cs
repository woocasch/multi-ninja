namespace MultiNinja.Backend.WebApi.Orchestration.Accounts;

public sealed class CreateTokenResponse
{
    public CreateTokenResponse(string token)
    {
        this.Token = token;
    }

    public string Token { get; }
}