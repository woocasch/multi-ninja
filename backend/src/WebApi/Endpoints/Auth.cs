using Microsoft.AspNetCore.Mvc;
using MultiNinja.Backend.Application;
using MultiNinja.Backend.Application.Security;
using MultiNinja.Backend.WebApi.Endpoints.AuthModels;

namespace MultiNinja.Backend.WebApi.Endpoints;

public static class Auth
{
    public static async Task<IResult> CreateAccount(
        [FromBody]CreateAccountInput input,
        ISecurityService securityService,
        CancellationToken cancellationToken)
    {
        var request = new CreateCredentialsRequest(input.Email, input.Password);
        var response = await securityService.CreateCredentials(request, cancellationToken);
        if (response.Result != CreateCredentialsResult.Created)
        {
            return Results.InternalServerError();
        }

        var result = new CreateAccountOutput(response.Id);
        return Results.Accepted($"api/auth/{result.Id}", result);
    }
}