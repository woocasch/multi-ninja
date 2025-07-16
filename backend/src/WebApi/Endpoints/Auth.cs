using Microsoft.AspNetCore.Mvc;
using MultiNinja.Backend.Application.Controllers;
using MultiNinja.Backend.Application.Controllers.Authentication;
using MultiNinja.Backend.Application.Logic.Security;
using MultiNinja.Backend.WebApi.Endpoints.AuthModels;

namespace MultiNinja.Backend.WebApi.Endpoints;

public static class Auth
{
    public static async Task<IResult> CreateAccount(
        [FromBody]CreateAccountInput input,
        IAuthenticationController authenticationController,
        CancellationToken cancellationToken)
    {
        var request = new CreateAccountRequest(input.Email, input.Password, input.DisplayName);
        var response = await authenticationController.CreateAccount(request, cancellationToken);
        if (!response.Success)
        {
            return Results.InternalServerError();
        }

        var result = new CreateAccountOutput(response.Id);
        return Results.Accepted($"api/auth/{result.Id}", result);
    }
}