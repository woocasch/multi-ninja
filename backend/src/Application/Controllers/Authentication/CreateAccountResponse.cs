namespace MultiNinja.Backend.Application.Controllers.Authentication;

public class CreateAccountResponse
{
    public CreateAccountResponse(Guid id)
    {
        this.Id = id;
    }

    public Guid Id { get; }

}