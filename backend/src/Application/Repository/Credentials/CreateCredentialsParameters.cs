namespace MultiNinja.Backend.Application.Repository.Credentials;

public class CreateCredentialsParameters
{
    public CreateCredentialsParameters(Guid id, string email, string password)
    {
        this.Id = id;
        this.Email = email;
        this.Password =  password;
    }

    public Guid Id { get; }

    public string Email { get; }
    
    public string Password { get; }
}