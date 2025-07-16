using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
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
        var createAccountPayload = AuthFakes.GetCreateAccountInput();
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/auth");
        var createAccountPayloadSerialized = JsonSerializer.Serialize(createAccountPayload);
        requestMessage.Content = new StringContent(createAccountPayloadSerialized, Encoding.UTF8, "application/json");
        var responseMessage = await this.client.Value.SendAsync(requestMessage);
        responseMessage.ShouldNotBeNull();
        responseMessage.StatusCode.ShouldBe(HttpStatusCode.OK);
        var responseContent = await responseMessage.Content.ReadAsStringAsync();
        responseContent.ShouldNotBeNull();
        responseContent.ShouldBe($"\"Created '{createAccountPayload.DisplayName}'\"");
    }

    private HttpClient CreateClient()
    {
        return this.webApiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = true,
        });
    }
}