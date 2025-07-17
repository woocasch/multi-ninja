using MultiNinja.Backend.Application.Orchestration.Accounts;

namespace MultiNinja.Backend.Application.Orchestration;

public interface IAccountsService
{
    Task<OneOf<CreateAccountResponse, ErrorData>> CreateAccount(
        CreateAccountRequest request,
        CancellationToken cancellationToken);
    
    Task<CreateTokenResponse?> CreateToken(
        CreateTokenRequest request,
        CancellationToken cancellationToken);
}