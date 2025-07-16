using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace MultiNinja.Backend.Application.Controllers.Authentication;

public class CreateAccountResponse
{
    private readonly Guid? id;

    private CreateAccountResponse(Guid? id, IEnumerable<Error>? errors)
    {
        this.id = id;
        this.Errors = new((errors ?? []).ToList());
    }

    public bool Success => this.Errors.Count == 0 && this.id.HasValue;

    public Guid Id
    {
        get
        {
            if (!this.Success)
            {
                throw new InvalidOperationException(
                    $"'{nameof(this.Id)}' is only available when operation succeeded. Check '{nameof(this.Success)}' before calling '{nameof(this.Id)}'.");
            }

            return this.id!.Value;
        }
    }

    public Collection<Error> Errors
    {
        get;
    }

    public static CreateAccountResponse Created(Guid id) => new(id, []);
    
    public static CreateAccountResponse Failed(params Error[] errors) => new(null, errors);

    public class Error
    {
        public Error(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public string Code { get; }
        
        public string Message { get; }
    }
}