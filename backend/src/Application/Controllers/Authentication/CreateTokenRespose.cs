namespace MultiNinja.Backend.Application.Controllers.Authentication;

public class CreateTokenRespose
{
    public CreateTokenRespose(string token)
    {
        this.Token = token;
    }

    public string Token { get; }
}