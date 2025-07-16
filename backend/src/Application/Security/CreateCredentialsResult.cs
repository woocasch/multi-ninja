namespace MultiNinja.Backend.Application.Security;

public enum CreateCredentialsResult
{
    Created = 0,
    
    EmailTaken = 1,
    
    UnknownError = 99,
}