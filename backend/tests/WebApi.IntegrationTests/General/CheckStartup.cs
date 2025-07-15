using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace MultiNinja.Backend.WebApi.IntegrationTests.General;

public class CheckStartup : IClassFixture<WebApiFactory>
{
    private readonly WebApiFactory webApiFactory;
    
    private readonly HttpClient client;

    public CheckStartup(WebApiFactory webApiFactory)
    {
        this.webApiFactory = webApiFactory;
        this.client = this.webApiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = true,
        });
    }

    [Fact]
    public async Task WhenEmptyRouteIsCalledThenCorrectResponseIsReturned()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/");
        var responseMessage = await client.SendAsync(requestMessage);
        responseMessage.ShouldNotBeNull();
        responseMessage.IsSuccessStatusCode.ShouldBeTrue();
        var contents = await responseMessage.Content.ReadAsStringAsync();
        contents.ShouldBe("Hello World!");
    }
}