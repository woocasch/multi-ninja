namespace MultiNinja.Backend.Application.Logic.Security;

public class CreateCredentialsResponse
{
    public CreateCredentialsResponse(
        Guid id)
    {
        this.Id = id;
    }

    public Guid Id { get; }
}