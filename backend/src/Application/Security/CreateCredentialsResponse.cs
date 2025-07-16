using System.Runtime.CompilerServices;

namespace MultiNinja.Backend.Application.Security;

public class CreateCredentialsResponse
{
    private readonly Guid? id;
    
    private CreateCredentialsResponse(
        CreateCredentialsResult result,
        Guid? id)
    {
        this.Result = result;
        this.id = id;
    }

    public CreateCredentialsResult Result { get; }

    public Guid Id
    {
        get
        {
            if (this.Result != CreateCredentialsResult.Created)
            {
                throw new InvalidOperationException(
                    $"This property should only be accessed when credentials were created (check '{nameof(this.Result)}' property first");
            }

            return this.id!.Value;
        }
    }

    public static CreateCredentialsResponse Created(Guid id) => new(CreateCredentialsResult.Created, id);

    public static CreateCredentialsResponse EmailTaken() => new(CreateCredentialsResult.EmailTaken, null);

    public static CreateCredentialsResponse UnknownError() => new(CreateCredentialsResult.UnknownError, null);
}