using System.Security.Cryptography;

namespace MultiNinja.Backend.Application.Cryptography;

public sealed class PasswordsCryptography : IPasswordsCryptography
{
    public async Task<byte[]> GeneratePasswordSalt(CancellationToken cancellationToken)
    {
        await Task.Yield();
        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    public async Task<byte[]> GeneratePasswordHash(string password, byte[] salt, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var pbkdf2Bytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        return pbkdf2Bytes.GetBytes(64);
    }
}