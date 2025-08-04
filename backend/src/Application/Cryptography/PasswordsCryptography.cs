namespace MultiNinja.Backend.Application.Cryptography;

public sealed class PasswordsCryptography : IPasswordsCryptography
{
    public async Task<string> GeneratePasswordSalt(CancellationToken cancellationToken)
    {
        await Task.Yield();
        return "PASSWORD_SALT";
    }

    public async Task<string> GeneratePasswordHash(string password, string salt, CancellationToken cancellationToken)
    {
        await Task.Yield();
        return $"{salt}_{password}";
    }
}