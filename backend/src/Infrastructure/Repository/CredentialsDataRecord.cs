namespace MultiNinja.Backend.Infrastructure.Repository;

public sealed class CredentialsDataRecord
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}