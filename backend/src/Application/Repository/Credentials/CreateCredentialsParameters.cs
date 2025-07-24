namespace MultiNinja.Backend.Application.Repository.Credentials;

public sealed class CreateCredentialsParameters
{
    public CreateCredentialsParameters(
        Guid id,
        Guid userId,
        string email,
        string password)
    {
        this.Id = id;
        this.UserId = userId;
        this.Email = email;
        this.Password =  password;
    }

    public Guid Id { get; }

    public Guid UserId { get; }

    public string Email { get; }
    
    public string Password { get; }
}