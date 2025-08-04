using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application;
using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;
using MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;

namespace MultiNinja.Backend.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services
            .AddApplication()
            .AddEfCoreWriteModel()
            .AddEfCoreReadModel();
        return services;
    }
}