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
    [InlineData("/Api/Brewery/{0}/Details")]
    public async Task GetBreweryDetails_ReturnsDetails(string url)
    {
        var client = _factory.CreateClient();

        var brewery = await GetBrewery(client);
        var details = await client.GetFromJsonAsync<BreweryDto>(string.Format(url, brewery?.Id));

        details.Should().NotBeNull();
        details?.Id.Should().NotBeNullOrWhiteSpace();
        details?.Name.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("/Api/Brewery/{0}/Beers")]
    public async Task GetBreweryBeers_ReturnsBeers(string url)
    {
        var client = _factory.CreateClient();

        var brewery = await GetBrewery(client);
        var beers = await client.GetFromJsonAsync<List<BeerDto>>(string.Format(url, brewery?.Id));

        beers.Should().NotBeNull();
        beers?.Count.Should().BeGreaterThan(0);
        beers?[0].Should().NotBeNull();
        beers?[0].Name.Should().NotBeNullOrWhiteSpace();
        beers?[0].Id.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("/Api/Brewery/{0}/Brewers")]
    public async Task GetBreweryBrewers_ReturnsBrewers(string url)
    {
        var client = _factory.CreateClient();

        var brewery = await GetBrewery(client);
        var brewers = await client.GetFromJsonAsync<List<BrewerDto>>(string.Format(url, brewery?.Id));

        brewers.Should().NotBeNull();
        brewers?.Count.Should().BeGreaterThan(0);
        brewers?[0].Id.Should().NotBeNullOrWhiteSpace();
        brewers?[0].FirstName.Should().NotBeNullOrWhiteSpace();
    }

    private static async Task<BreweryDto?> GetBrewery(HttpClient client)
    {
        var breweries = await client.GetFromJsonAsync<List<BreweryDto>>("/Api/Brewery/All");
        return breweries?[0];
    }
}
