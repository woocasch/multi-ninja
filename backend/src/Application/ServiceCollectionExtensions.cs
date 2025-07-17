using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.Orchestration;

namespace MultiNinja.Backend.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddOrchestration()
            .AddSingleton<IMediator, Mediator>()
            .AddKeyedTransient<ICommandHandler, Security.CreateCredentialsCommandHandler>(typeof(Security.CreateCredentialsCommand).AssemblyQualifiedName)
            .AddKeyedTransient<IQueryHandler<Security.VerifyCredentialsResult>, Security.VerifyCredentialsQueryHandler>(typeof(Security.VerifyCredentialsQuery).AssemblyQualifiedName);
        return services;
    }
}