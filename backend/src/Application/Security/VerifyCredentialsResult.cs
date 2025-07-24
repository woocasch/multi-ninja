namespace MultiNinja.Backend.Application.Security;

public sealed class VerifyCredentialsResult
{
    public VerifyCredentialsResult(Guid? id)
    {
        this.Id = id;
    }

    public Guid? Id { get; }
}