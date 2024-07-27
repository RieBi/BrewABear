using FluentAssertions;

namespace IntegrationTests;
public class BasicTests(ApiApplicationFactory factory) : IClassFixture<ApiApplicationFactory>
{
    private readonly ApiApplicationFactory _factory = factory;

    [Theory]
    [InlineData("/Api/Wholesaler/All")]
    [InlineData("/Api/Brewery/All")]
    [InlineData("/Api/Order/All")]
    public async Task Get_EndpointsReturnsSuccess(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
    }

    [Theory]
    [InlineData("/NotApi/123")]
    [InlineData("/Api/Brewery/Nonexisting")]
    [InlineData("/Api/Nonexisting")]
    public async Task GetNonExisting_ReturnsNotFound(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        var body = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        body.Should().Be(string.Empty);
    }
}
