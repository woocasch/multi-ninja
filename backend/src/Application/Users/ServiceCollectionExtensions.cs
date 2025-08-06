using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application.Users;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(
        this IServiceCollection services)
    {
        services
            .RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>()
            .RegisterQueryHandler<GetUserByUserNameQuery, GetUserResult, GetUserByUserNameQueryHandler>();
        return services;
    }
}