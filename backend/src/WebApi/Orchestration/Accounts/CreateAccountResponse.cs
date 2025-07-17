namespace MultiNinja.Backend.WebApi.Orchestration.Accounts;

public class CreateAccountResponse
{
    public CreateAccountResponse(Guid id)
    {
        this.Id = id;
    }

    public Guid Id { get; }

}