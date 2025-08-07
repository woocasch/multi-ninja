namespace MultiNinja.Backend.WebApi.Endpoints;

public static class AuthExtensions
{
    public static WebApplication MapAuth(
        this WebApplication app)
    {
        app
            .MapPost("/api/auth/ensureAccountCreated", Auth.EnsureAccountCreated)
            .RequireAuthorization();

        app
            .MapPost("/api/auth/createToken", Auth.CreateToken);
        return app;
    }
}