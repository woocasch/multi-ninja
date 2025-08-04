using Microsoft.Extensions.DependencyInjection;

namespace MultiNinja.Backend.Application.Cryptography;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCryptography(
        this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordsCryptography, PasswordsCryptography>();
        return services;
    }
}