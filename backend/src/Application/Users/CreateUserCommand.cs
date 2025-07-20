namespace MultiNinja.Backend.Application.Users;

public sealed class CreateUserCommand : ICommand
{
    public CreateUserCommand(Guid id, string displayName)
    {
        this.Id = id;
        this.DisplayName = displayName;
    }

    public Guid Id { get; }

    public string DisplayName { get; }
}