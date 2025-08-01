using Microsoft.EntityFrameworkCore;
using MultiNinja.Backend.Application;
using MultiNinja.Backend.Application.Security;
using MultiNinja.Backend.Application.Users;
using MultiNinja.Backend.Infrastructure.WriteModel.EfCore;
using MultiNinja.Backend.WebApi.Orchestration.Accounts;

namespace MultiNinja.Backend.WebApi.Orchestration;

public sealed class AccountsService : IAccountsService
{
    private readonly IMediator mediator;

    private readonly WriteContext writeContext;

    public AccountsService(
        IMediator mediator,
        WriteContext writeContext)
    {
        this.mediator = mediator;
        this.writeContext = writeContext;
    }

    public async Task<OneOf<CreateAccountResponse, ErrorData>> CreateAccount(
        CreateAccountRequest request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var createUserCommand = new CreateUserCommand(userId, request.DisplayName);
        var createUserResult = await this.mediator.Execute(createUserCommand, cancellationToken);
        if (!createUserResult.Success)
        {
            return new ErrorData([new ErrorData.ErrorMessage("UserCreationError", "User creation error")]);
        }

        var credentialsId = Guid.NewGuid();
        var createCredentialsCommand =
            new CreateCredentialsCommand(credentialsId, userId, request.Email, request.Password);
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

        await this.writeContext.SaveChangesAsync(cancellationToken);
        return new CreateAccountResponse(credentialsId);
    }

    public async Task<CreateTokenResponse?> CreateToken(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        var query = new VerifyCredentialsQuery(request.Email, request.Password);
        var result = await this.mediator.Fetch(query, cancellationToken);
        if (result.Id is null)
        {
            return null;
        }

        return new("gpdsamgodsapmsogps");
    }
}