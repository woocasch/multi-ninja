namespace MultiNinja.Backend.WebApi.Orchestration.Accounts;

public sealed class CreateAccountRequest
{
    public CreateAccountRequest(string userName, string password, string displayName)
    {
        this.UserName = userName;
        this.Password = password;
        this.DisplayName = displayName;
    }

    public string UserName { get; }

    public string Password { get; }

    public string DisplayName { get; }
}