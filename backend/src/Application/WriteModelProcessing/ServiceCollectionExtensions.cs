using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.WriteModelProcessing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWriteModelProcessing(
        this IServiceCollection services)
    {
        services
            .AddEventHandler<Domain.Users.UserCreated, User.UserCreatedHandler>()
            .AddEventHandler<Domain.Credentials.CredentialsCreated, Credentials.CredentialsCreatedHandler>()
            .AddTransient<IProcessor, Processor>();
        return services;
    }

    private static IServiceCollection AddEventHandler<TEvent, THandler>(
        this IServiceCollection services)
        where TEvent : EntityEvent
        where THandler : class, IEventHandler
    {
        services
            .AddKeyedTransient<IEventHandler, THandler>(typeof(TEvent).AssemblyQualifiedName);
        return services;
    }
}