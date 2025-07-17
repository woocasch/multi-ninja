using System.Collections.ObjectModel;

namespace MultiNinja.Backend.WebApi.Orchestration;

public class ErrorData
{
    public ErrorData(IEnumerable<ErrorMessage> errors)
    {
        this.Errors = new(errors.ToList());
    }
    
    public ReadOnlyCollection<ErrorMessage> Errors { get; }

    public class ErrorMessage
    {
        public ErrorMessage(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public string Code { get; }
        
        public string Message { get; }
    }
}