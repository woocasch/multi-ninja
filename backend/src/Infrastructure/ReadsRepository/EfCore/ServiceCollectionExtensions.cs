using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.ReadsRepository;

namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEfCoreReadModel(
        this IServiceCollection services)
    {
        services
            .AddScoped<ICredentials, CredentialsRepository>();
        return services;
    }
}