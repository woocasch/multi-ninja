using MultiNinja.Backend.Application.Controllers.Authentication;

namespace MultiNinja.Backend.Application.Controllers;

public interface IAuthenticationController
{
    Task<OneOf<CreateAccountResponse, ErrorData>> CreateAccount(
        CreateAccountRequest request,
        CancellationToken cancellationToken);
    
    Task<CreateTokenRespose?> CreateToken(
        CreateTokenRequest request,
        CancellationToken cancellationToken);
}