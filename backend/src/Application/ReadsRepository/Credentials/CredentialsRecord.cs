namespace MultiNinja.Backend.Application.ReadsRepository.Credentials;

public sealed class CredentialsRecord
{
    public CredentialsRecord(
        Guid id,
        Guid userId,
        string email)
    {
        this.Id = id;
        this.UserId = userId;
        this.Email = email;
    }

    public Guid Id { get; }

    public Guid UserId { get; }

    public string Email { get; }
}