using Microsoft.Extensions.DependencyInjection;
using MultiNinja.Backend.Application.EventStreams;
using MultiNinja.Backend.Application.WriteModelProcessing;
using MultiNinja.Backend.Application.Security;
using MultiNinja.Backend.Application.Users;

namespace MultiNinja.Backend.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddSecurity()
            .AddUsers() 
            .AddEventStreams()
            .AddWriteModelProcessing()
            .AddScoped<IMediator, Mediator>();
        return services;
    }

    public static IServiceCollection RegisterCommandHandler<TCommand, TCommandHandler>(
        this IServiceCollection services)
        where TCommand : ICommand
        where TCommandHandler : class, ICommandHandler
    {
        return services.AddKeyedTransient<ICommandHandler, TCommandHandler>(typeof(TCommand).AssemblyQualifiedName);
    }

    public static IServiceCollection RegisterQueryHandler<TQuery, TResult, TQueryHandler>(
        this IServiceCollection services)
        where TResult : class
        where TQuery : IQuery<TResult>
        where TQueryHandler : class, IQueryHandler<TResult>
    {
        return services.AddKeyedTransient<IQueryHandler<TResult>, TQueryHandler>(typeof(TQuery).AssemblyQualifiedName);
    }
}