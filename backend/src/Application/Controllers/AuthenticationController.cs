using MultiNinja.Backend.Application.Controllers.Authentication;
using MultiNinja.Backend.Application.Logic;
using MultiNinja.Backend.Application.Logic.Security;

namespace MultiNinja.Backend.Application.Controllers;

public class AuthenticationController : IAuthenticationController
{
    private readonly ISecurityService securityService;

    public AuthenticationController(ISecurityService securityService)
    {
        this.securityService = securityService;
    }

    public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var createCredentialsRequest = new CreateCredentialsRequest(request.Email, request.Password);
        var createCredentialsResponse = await this.securityService.CreateCredentials(createCredentialsRequest, cancellationToken);
        if (createCredentialsResponse.Result == CreateCredentialsResult.EmailTaken)
        {
            var error = new CreateAccountResponse.Error("EmailTaken", "Email already taken");
            return CreateAccountResponse.Failed(error);
        }

        if (createCredentialsResponse.Result == CreateCredentialsResult.UnknownError)
        {
            var error = new CreateAccountResponse.Error("UnknownError", "Unknown error");
            return CreateAccountResponse.Failed(error);
        }
        
        return CreateAccountResponse.Created(createCredentialsResponse.Id);
    }
}