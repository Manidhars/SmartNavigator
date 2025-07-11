using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SmartNavigator.Services;
using Xunit;

class FakeHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _response;

    public FakeHandler(HttpResponseMessage response)
    {
        _response = response;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_response);
    }
}

public class AiClientTests
{
    [Fact]
    public async Task QueryAsync_ReturnsGeneratedText()
    {
        var json = "[{\"generated_text\":\"Hello world\"}]";
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };
        var handler = new FakeHandler(response);
        var httpClient = new HttpClient(handler);
        var client = new AiClient(httpClient, "token");

        var result = await client.QueryAsync("test");

        Assert.Equal("Hello world", result);
    }

    [Fact]
    public async Task QueryAsync_ReturnsEmptyWhenNull()
    {
        var json = "[{\"generated_text\":null}]";
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };
        var handler = new FakeHandler(response);
        var httpClient = new HttpClient(handler);
        var client = new AiClient(httpClient, "token");

        var result = await client.QueryAsync("test");

        Assert.Equal(string.Empty, result);
    }
}
