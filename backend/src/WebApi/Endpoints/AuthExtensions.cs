namespace MultiNinja.Backend.WebApi.Endpoints;

public static class AuthExtensions
{
    public static WebApplication MapAuth(
        this WebApplication app)
    {
        app
            .MapPost("/api/auth", Auth.CreateAccount);

        app
            .MapPost("/api/auth/createToken", Auth.CreateToken);
        return app;
    }
}