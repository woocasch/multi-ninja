namespace MultiNinja.Backend.Application.Logic.Security;

public enum CreateCredentialsResult
{
    Created = 0,
    
    EmailTaken = 1,
    
    UnknownError = 99,
}