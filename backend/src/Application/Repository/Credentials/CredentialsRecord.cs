namespace MultiNinja.Backend.Application.Repository.Credentials;

public sealed class CredentialsRecord
{
    public CredentialsRecord(Guid id, string email)
    {
        this.Id = id;
        this.Email = email;
    }

    public Guid Id { get; }

    public string Email { get; }
}