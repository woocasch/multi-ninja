namespace MultiNinja.Backend.Application.Security;

public class VerifyCredentialsResult
{
    public VerifyCredentialsResult(Guid? id)
    {
        this.Id = id;
    }

    public Guid? Id { get; }
}