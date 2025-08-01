using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Infrastructure.Repository;
using MultiNinja.Backend.Infrastructure.WriteModel.EfCore;

namespace MultiNinja.Backend.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services
            .AddApplication()
            .AddSingleton<ICredentials, CredentialsRepository>()
            .AddSingleton<IUsers, UsersRepository>()
            .AddEfCoreWriteModel();
        return services;
    }
}