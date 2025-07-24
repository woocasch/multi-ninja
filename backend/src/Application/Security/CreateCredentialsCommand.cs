namespace MultiNinja.Backend.Application.Security;

public sealed class CreateCredentialsCommand : ICommand
{
    public CreateCredentialsCommand(
        Guid id,
        Guid userId,
        string email,
        string password)
    {
        this.Id = id;
        this.UserId = userId;
        this.Email = email;
        this.Password = password;
    }
    
    public Guid Id { get; }

    public Guid UserId { get; }

    public string Email { get; }

    public string Password { get; }
}