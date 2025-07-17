using MultiNinja.Backend.Application.Orchestration.Accounts;
using MultiNinja.Backend.Application.Logic;
using MultiNinja.Backend.Application.Logic.Security;

namespace MultiNinja.Backend.Application.Orchestration;

public class AccountsService : IAccountsService
{
    private readonly ISecurityService securityService;

    public AccountsService(ISecurityService securityService)
    {
        this.securityService = securityService;
    }

    public async Task<OneOf<CreateAccountResponse, ErrorData>> CreateAccount(
        CreateAccountRequest request,
        CancellationToken cancellationToken)
    {
        var createCredentialsRequest = new CreateCredentialsRequest(request.Email, request.Password);
        var createCredentialsResponse = await this.securityService.CreateCredentials(
            createCredentialsRequest,
            cancellationToken);
        if (createCredentialsResponse is { IsT1: true, AsT1: CreateCredentialsError.EmailTaken })
        {
            var error = new ErrorData.ErrorMessage("EmailTaken", "Email already taken");
            return new ErrorData([error]);
        }

        if (createCredentialsResponse is { IsT1: true, AsT1: CreateCredentialsError.UnknownError })
        {
            var error = new ErrorData.ErrorMessage("UnknownError", "Unknown error");
            return new ErrorData([error]);
        }

        if (createCredentialsResponse.IsT1)
        {
            var error = new ErrorData.ErrorMessage("UnknownError", $"Unknown error '{createCredentialsResponse.AsT1}'.");
            return new ErrorData([error]);
        }

        return new CreateAccountResponse(createCredentialsResponse.AsT0.Id);
    }

    public async Task<CreateTokenResponse?> CreateToken(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        var verifyCredentialsRequest = new VerifyCredentialsRequest(request.Email, request.Password);
        var verifyCredentialsResponse = await this.securityService.VerifyCredentials(verifyCredentialsRequest, cancellationToken);
        if (verifyCredentialsResponse is null)
        {
            return null;
        }

        return new("gpdsamgodsapmsogps");
    }

}