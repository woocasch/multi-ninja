using Microsoft.AspNetCore.Mvc;
using MultiNinja.Backend.WebApi.Endpoints.AuthModels;

namespace MultiNinja.Backend.WebApi.Endpoints;

public class Auth
{
    public static IResult CreateAccount([FromBody]CreateAccountInput input)
    {
        return Results.Ok($"Created '{input.DisplayName}'");
    }
}