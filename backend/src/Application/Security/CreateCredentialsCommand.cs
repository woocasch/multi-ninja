namespace MultiNinja.Backend.Application.Security;

public sealed class CreateCredentialsCommand : ICommand
{
    public CreateCredentialsCommand(
        Guid id,
        string email,
        string password)
    {
        this.Id = id;
        this.Email = email;
        this.Password = password;
    }
    
    public Guid Id { get; }

    public string Email { get; }

    public string Password { get; }
}