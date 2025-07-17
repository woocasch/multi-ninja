using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MultiNinja.Backend.WebApi.Endpoints.AuthModels;
using Shouldly;

namespace MultiNinja.Backend.WebApi.IntegrationTests.Scenarios;

public class AuthTests : IClassFixture<WebApiFactory>
{
    private readonly WebApiFactory webApiFactory;

    private readonly Lazy<HttpClient> client;

    public AuthTests(WebApiFactory webApiFactory)
    {
        this.webApiFactory = webApiFactory;
        this.client = new(this.CreateClient);
    }

    [Fact]
    public async Task WhenUserIsCreatedThenCorrectResponseIsReturned()
    {
        var createAccountInput = AuthFakes.GetCreateAccountInput();
        var createAccountRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth");
        var createAccountPayloadSerialized = SerializationProvider.Serialize(createAccountInput);
        createAccountRequest.Content = new StringContent(createAccountPayloadSerialized, Encoding.UTF8, "application/json");
        var createAccountResponse = await this.client.Value.SendAsync(createAccountRequest);
        createAccountResponse.ShouldNotBeNull();
        createAccountResponse.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        var createAccountResponseContent = await createAccountResponse.Content.ReadAsStringAsync();
        createAccountResponseContent.ShouldNotBeNull();
        var createAccountOutput = SerializationProvider.Deserialize<CreateAccountOutput>(createAccountResponseContent);
        createAccountOutput.ShouldNotBeNull();
        createAccountOutput.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task WhenUserIsCreatedThenUserTokenCanBeGenerated()
    {
        var createAccountInput = AuthFakes.GetCreateAccountInput();
        var createAccountRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth");
        var createAccountPayloadSerialized = SerializationProvider.Serialize(createAccountInput);
        createAccountRequest.Content = new StringContent(createAccountPayloadSerialized, Encoding.UTF8, "application/json");
        var createAccountResponse = await this.client.Value.SendAsync(createAccountRequest);
        createAccountResponse.ShouldNotBeNull();
        
        var createTokenInput = new CreateTokenInput(createAccountInput.Email, createAccountInput.Password);
        var createTokenRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/createToken");
        var createTokenPayload = SerializationProvider.Serialize(createTokenInput);
        createTokenRequest.Content = new StringContent(createTokenPayload, Encoding.UTF8, "application/json");
        var createTokenResponse = await this.client.Value.SendAsync(createTokenRequest);
        createTokenResponse.ShouldNotBeNull();
        createTokenResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createTokenResponseContent = await createTokenResponse.Content.ReadAsStringAsync();
        var createTokenOutput = SerializationProvider.Deserialize<CreateTokenOutput>(createTokenResponseContent);
        createTokenOutput.ShouldNotBeNull();
        createTokenOutput.Token.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task WhenInvalidEmailIsProvidedThenUserTokenCanBeGenerated()
    {
        var createAccountInput = AuthFakes.GetCreateAccountInput();
        var createAccountRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth");
        var createAccountPayloadSerialized = SerializationProvider.Serialize(createAccountInput);
        createAccountRequest.Content = new StringContent(createAccountPayloadSerialized, Encoding.UTF8, "application/json");
        var createAccountResponse = await this.client.Value.SendAsync(createAccountRequest);
        createAccountResponse.ShouldNotBeNull();
        
        var createTokenInput = new CreateTokenInput($"A{createAccountInput.Email}", createAccountInput.Password);
        var createTokenRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/createToken");
        var createTokenPayload = SerializationProvider.Serialize(createTokenInput);
        createTokenRequest.Content = new StringContent(createTokenPayload, Encoding.UTF8, "application/json");
        var createTokenResponse = await this.client.Value.SendAsync(createTokenRequest);
        createTokenResponse.ShouldNotBeNull();
        createTokenResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task WhenInvalidPasswordIsProvidedThenUserTokenCanBeGenerated()
    {
        var createAccountInput = AuthFakes.GetCreateAccountInput();
        var createAccountRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth");
        var createAccountPayloadSerialized = SerializationProvider.Serialize(createAccountInput);
        createAccountRequest.Content = new StringContent(createAccountPayloadSerialized, Encoding.UTF8, "application/json");
        var createAccountResponse = await this.client.Value.SendAsync(createAccountRequest);
        createAccountResponse.ShouldNotBeNull();
        
        var createTokenInput = new CreateTokenInput($"A{createAccountInput.Email}", createAccountInput.Password);
        var createTokenRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/createToken");
        var createTokenPayload = SerializationProvider.Serialize(createTokenInput);
        createTokenRequest.Content = new StringContent(createTokenPayload, Encoding.UTF8, "application/json");
        var createTokenResponse = await this.client.Value.SendAsync(createTokenRequest);
        createTokenResponse.ShouldNotBeNull();
        createTokenResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    private HttpClient CreateClient()
    {
        return this.webApiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = true,
        });
    }
}