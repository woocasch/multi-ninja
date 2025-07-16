using MultiNinja.Backend.Application.Controllers.Authentication;

namespace MultiNinja.Backend.Application.Controllers;

public interface IAuthenticationController
{
    Task<CreateAccountResponse> CreateAccount(
        CreateAccountRequest request,
        CancellationToken cancellationToken);
}