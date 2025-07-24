using Microsoft.AspNetCore.Mvc;
using MultiNinja.Backend.WebApi.Endpoints.AuthModels;
using MultiNinja.Backend.WebApi.Orchestration;
using MultiNinja.Backend.WebApi.Orchestration.Accounts;

namespace MultiNinja.Backend.WebApi.Endpoints;

public static class Auth
{
    public static async Task<IResult> CreateAccount(
        [FromBody]CreateAccountInput input,
        IAccountsService authenticationController,
        CancellationToken cancellationToken)
    {
        var request = new CreateAccountRequest(input.Email, input.Password, input.DisplayName);
        var response = await authenticationController.CreateAccount(request, cancellationToken);
        return response.Match(
            r => Results.Accepted($"/api/auth/{r.Id}", new CreateAccountOutput(r.Id)),
            _ => Results.InternalServerError());
    }

    public static async Task<IResult> CreateToken(
        [FromBody] CreateTokenInput input,
        IAccountsService accountsService,
        CancellationToken cancellationToken)
    {
        var request = new CreateTokenRequest(input.Email, input.Password);
        var response = await accountsService.CreateToken(request, cancellationToken);
        if (response is null)
        {
            return Results.BadRequest();
        }

        return Results.Ok(new CreateTokenOutput(response.Token));
    }
}