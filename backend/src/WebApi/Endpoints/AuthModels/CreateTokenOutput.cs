namespace MultiNinja.Backend.WebApi.Endpoints.AuthModels;

public sealed class CreateTokenOutput
{
    public CreateTokenOutput(
        string userName,
        string displayName)
    {
        this.UserName = userName;
        this.DisplayName = displayName;
    }

    public string UserName { get; }

    public string DisplayName { get; }
}