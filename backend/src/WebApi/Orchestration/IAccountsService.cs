using MultiNinja.Backend.WebApi.Orchestration.Accounts;

namespace MultiNinja.Backend.WebApi.Orchestration;

public interface IAccountsService
{
    Task<OneOf<CreateAccountResponse, ErrorData>> CreateAccount(
        CreateAccountRequest request,
        CancellationToken cancellationToken);
    
    Task<CreateTokenResponse?> CreateToken(
        CreateTokenRequest request,
        CancellationToken cancellationToken);
}