using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.WritesRepository;

namespace MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEfCoreWriteModel(
        this IServiceCollection services)
    {
        services
            .AddScoped<IStreams, StreamsRepository>();
        return services;
    }
}