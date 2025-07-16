using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application.Controllers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddControllers(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationController, AuthenticationController>();
        return services;
    }
}