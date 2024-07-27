using Application.DTOs;
using FluentAssertions;
using System.Net.Http.Json;

namespace IntegrationTests.ControllerTests;
public class BreweryControllerTests(ApiApplicationFactory factory) : IClassFixture<ApiApplicationFactory>
{
    private readonly ApiApplicationFactory _factory = factory;

    [Theory]
    [InlineData("/Api/Brewery/All")]
    public async Task GetAllBreweries_ReturnsObjects(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetFromJsonAsync<List<BrewerDto>>(url);

        response.Should().NotBeNull();
        response?[0].Should().NotBeNull();
        response?[0].Id.Should().NotBeNull();
    }

    [Theory]
    [InlineData("/Api/Brewery/All", "/Api/Brewery/{0}/Details")]
    public async Task GetBreweryDetails_ReturnsDetails(string allUrl, string url)
    {
        var client = _factory.CreateClient();

        var brewers = await client.GetFromJsonAsync<List<BreweryDto>>(allUrl);
        var details = await client.GetFromJsonAsync<BreweryDto>(string.Format(url, brewers?[0].Id));

        details.Should().NotBeNull();
        details?.Id.Should().NotBeNullOrWhiteSpace();
        details?.Name.Should().NotBeNullOrWhiteSpace();
    }
}
