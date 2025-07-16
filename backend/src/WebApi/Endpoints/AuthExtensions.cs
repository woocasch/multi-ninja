namespace MultiNinja.Backend.WebApi.Endpoints;

public static class AuthExtensions
{
    public static WebApplication MapAuth(
        this WebApplication app)
    {
        app.MapPost("/api/auth", Auth.CreateAccount);
        return app;
    }
}