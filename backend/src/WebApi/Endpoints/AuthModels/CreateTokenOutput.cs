namespace MultiNinja.Backend.WebApi.Endpoints.AuthModels;

public sealed class CreateTokenOutput
{
    public CreateTokenOutput(string token)
    {
        this.Token = token;
    }

    public string Token { get; }
}