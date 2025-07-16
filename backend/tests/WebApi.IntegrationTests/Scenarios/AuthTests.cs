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
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/auth");
        var createAccountPayloadSerialized = SerializationProvider.Serialize(createAccountInput);
        requestMessage.Content = new StringContent(createAccountPayloadSerialized, Encoding.UTF8, "application/json");
        var responseMessage = await this.client.Value.SendAsync(requestMessage);
        responseMessage.ShouldNotBeNull();
        responseMessage.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        var responseContent = await responseMessage.Content.ReadAsStringAsync();
        responseContent.ShouldNotBeNull();
        var createAccountOutput = SerializationProvider.Deserialize<CreateAccountOutput>(responseContent);
        createAccountOutput.ShouldNotBeNull();
        createAccountOutput.Id.ShouldNotBe(Guid.Empty);
    }

    private HttpClient CreateClient()
    {
        return this.webApiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = true,
        });
    }
}