namespace MultiNinja.Backend.Application.Cryptography;

public interface IPasswordsCryptography
{
    Task<byte[]> GeneratePasswordSalt(CancellationToken cancellationToken);
    
    Task<byte[]> GeneratePasswordHash(string password, byte[] salt, CancellationToken cancellationToken);
}