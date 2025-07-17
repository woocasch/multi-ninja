using MultiNinja.Backend.Application.Orchestration.Accounts;
using MultiNinja.Backend.Application.Logic;
using MultiNinja.Backend.Application.Logic.Security;
using MultiNinja.Backend.Application.Security;

namespace MultiNinja.Backend.Application.Orchestration;

public class AccountsService : IAccountsService
{
    private readonly ISecurityService securityService;
    
    private readonly IMediator mediator;

    public AccountsService(ISecurityService securityService, IMediator mediator)
    {
        this.securityService = securityService;
        this.mediator = mediator;
    }

    public async Task<OneOf<CreateAccountResponse, ErrorData>> CreateAccount(
        CreateAccountRequest request,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var createCredentialsCommand = new CreateCredentialsCommand(id, request.Email, request.Password);
        var result = await this.mediator.Execute(createCredentialsCommand, cancellationToken);
        if (!result.Success)
        {
            if (result.ErrorCode == CreateCredentialsError.EmailTaken)
            {
                var error = new ErrorData.ErrorMessage("EmailTaken", "Email already taken");
                return new ErrorData([error]);
            }

            if (result.ErrorCode == CreateCredentialsError.UnknownError)
            {
                var error = new ErrorData.ErrorMessage("UnknownError", "Unknown error");
                return new ErrorData([error]);
            }
        }

        return new CreateAccountResponse(id);
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