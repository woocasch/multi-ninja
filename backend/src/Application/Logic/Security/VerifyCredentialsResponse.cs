namespace MultiNinja.Backend.Application.Logic.Security;

public class VerifyCredentialsResponse
{
    public VerifyCredentialsResponse(Guid id)
    {
        this.Id = id;
    }

    public Guid Id { get; }
}