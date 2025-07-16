using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddTransient<ISecurityService, SecurityService>();
        return services;
    }
}