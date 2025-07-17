namespace MultiNinja.Backend.WebApi.Orchestration.Accounts;

public class CreateTokenResponse
{
    public CreateTokenResponse(string token)
    {
        this.Token = token;
    }

    public string Token { get; }
}