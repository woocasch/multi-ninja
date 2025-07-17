using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.Orchestration;
using MultiNinja.Backend.Application.Logic;

namespace MultiNinja.Backend.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddOrchestration()
            .AddLogic();
        return services;
    }
}