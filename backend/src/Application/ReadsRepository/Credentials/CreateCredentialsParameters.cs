namespace MultiNinja.Backend.Application.ReadsRepository.Credentials;

public sealed class CreateCredentialsParameters
{
    public CreateCredentialsParameters(
        Guid id,
        Guid userId,
        string userName,
        string password)
    {
        this.Id = id;
        this.UserId = userId;
        this.UserName = userName;
        this.Password =  password;
    }

    public Guid Id { get; }

    public Guid UserId { get; }

    public string UserName { get; }
    
    public string Password { get; }
}