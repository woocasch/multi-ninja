using System.Collections.ObjectModel;

namespace MultiNinja.Backend.Application;

public class CommandExecutionResult
{
    private CommandExecutionResult(
        bool success,
        string errorCode)
    {
        this.Success = success;
        this.ErrorCode = errorCode;
    }
    
    public bool Success { get; }

    public string ErrorCode { get; }

    public static CommandExecutionResult Succeeded() => new(true, string.Empty);
    
    public static CommandExecutionResult Failed(string errorCode) => new(false,  errorCode);
}