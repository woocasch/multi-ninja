using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Infrastructure.Repository;
using MultiNinja.Backend.Infrastructure.Repository.EfCore;

namespace MultiNinja.Backend.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services
            .AddApplication()
            .AddSingleton<ICredentials, CredentialsRepository>()
            .AddScoped<IStreams, StreamsRepository>()
            .AddSingleton<IUsers, UsersRepository>();
        return services;
    }
}