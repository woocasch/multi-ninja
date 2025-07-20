namespace MultiNinja.Backend.WebApi.Endpoints.AuthModels;

public sealed record CreateAccountInput(string Email, string Password, string DisplayName);