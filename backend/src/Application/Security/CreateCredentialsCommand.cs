namespace MultiNinja.Backend.Application.Security;

public sealed class CreateCredentialsCommand : ICommand
{
    public CreateCredentialsCommand(
        Guid id,
        Guid userId,
        string userName,
        string password)
    {
        this.Id = id;
        this.UserId = userId;
        this.UserName = userName;
        this.Password = password;
    }
    
    public Guid Id { get; }

    public Guid UserId { get; }

    public string UserName { get; }

    public string Password { get; }
}