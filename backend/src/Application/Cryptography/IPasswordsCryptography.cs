namespace MultiNinja.Backend.Application.Cryptography;

public interface IPasswordsCryptography
{
    Task<string> GeneratePasswordSalt(CancellationToken cancellationToken);
    
    Task<string> GeneratePasswordHash(string password, string salt, CancellationToken cancellationToken);
}