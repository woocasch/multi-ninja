namespace MultiNinja.Backend.Application.ReadsRepository.Credentials;

public sealed class CreateCredentialsParameters
{
    public CreateCredentialsParameters(
        Guid id,
        Guid userId,
        string userName)
    {
        this.Id = id;
        this.UserId = userId;
        this.UserName = userName;
    }

    public Guid Id { get; }

    public Guid UserId { get; }

    public string UserName { get; }
}