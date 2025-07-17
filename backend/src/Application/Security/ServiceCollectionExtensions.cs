using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application.Security;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSecurity(
        this IServiceCollection services)
    {
        services
            .RegisterCommandHandler<CreateCredentialsCommand, CreateCredentialsCommandHandler>()
            .RegisterQueryHandler<VerifyCredentialsQuery, VerifyCredentialsResult, VerifyCredentialsQueryHandler>();
        return services;
    }
}