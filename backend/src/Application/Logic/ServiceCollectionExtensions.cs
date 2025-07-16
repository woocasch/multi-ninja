using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.Logic;

namespace MultiNinja.Backend.Application.Logic;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogic(
        this IServiceCollection services)
    {
        services.AddTransient<ISecurityService, SecurityService>();
        return services;
    }
}