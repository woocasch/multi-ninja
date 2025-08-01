using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.Repository;

namespace MultiNinja.Backend.Infrastructure.WriteModel.EfCore;

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