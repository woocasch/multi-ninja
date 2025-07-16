namespace MultiNinja.Backend.WebApi.Endpoints.AuthModels;

public record CreateAccountInput(string Email, string Password, string DisplayName);