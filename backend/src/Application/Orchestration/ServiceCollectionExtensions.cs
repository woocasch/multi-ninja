using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application.Orchestration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrchestration(
        this IServiceCollection services)
    {
        services.AddScoped<IAccountsService, AccountsService>();
        return services;
    }
}