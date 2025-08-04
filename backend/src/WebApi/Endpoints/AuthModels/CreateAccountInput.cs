namespace MultiNinja.Backend.WebApi.Endpoints.AuthModels;

public sealed record CreateAccountInput(string UserName, string Password, string DisplayName);