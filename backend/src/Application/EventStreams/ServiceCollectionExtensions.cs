using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application.EventStreams;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventStreams(
        this IServiceCollection services)
    {
        services
            .AddScoped<IEventStreamsService, EventStreamService>();
        return services;
    }
}