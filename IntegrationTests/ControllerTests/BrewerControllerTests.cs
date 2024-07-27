using Application.DTOs;
using FluentAssertions;
using System.Net.Http.Json;

namespace IntegrationTests.ControllerTests;
public class BrewerControllerTests(ApiApplicationFactory factory) : IClassFixture<ApiApplicationFactory>
{
    private readonly ApiApplicationFactory _factory = factory;

    [Theory]
    [InlineData("/Api/Brewer/{0}/Beers")]
    public async Task GetBrewerBeers_ReturnsBeers(string url)
    {
        var client = _factory.CreateClient();
        var brewer = await GetBrewer(client);

        var response = await client.GetFromJsonAsync<List<BeerDto>>(string.Format(url, brewer?.Id));

        response.Should().NotBeNull();
        response?.Count.Should().BeGreaterThan(0);
        response?[0].Should().NotBeNull();
        response?[0].Name.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("/Api/Brewer/{0}/Details")]
    public async Task GetBrewerDetails_ReturnsDetails(string url)
    {
        var client = _factory.CreateClient();
        var brewer = await GetBrewer(client);

        var response = await client.GetFromJsonAsync<BrewerDto>(string.Format(url, brewer?.Id));

        response.Should().NotBeNull();
        response?.Id.Should().BeEquivalentTo(brewer?.Id);
        response?.FirstName.Should().NotBeNullOrWhiteSpace();
        response?.ContactEmail.Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("/Api/Brewer/{0}/AddBeer")]
    public async Task AddBeer_ReturnsCreatedBeer(string url)
    {
        var client = _factory.CreateClient();
        var brewer = await GetBrewer(client);

        var newBrewer = new BeerCreateDto()
        {
            Name = "Integration beer",
            Description = "Beer used in integration testing",
            Flavor = "Flavory",
            Price = 10,
        };

        var response = await client.PostAsJsonAsync(string.Format(url, brewer?.Id), newBrewer);
        var created = await response.Content.ReadFromJsonAsync<BeerDto>();

        response.EnsureSuccessStatusCode();
        created.Should().NotBeNull();
        created?.Id.Should().NotBeNullOrWhiteSpace();
        created?.Name.Should().BeEquivalentTo(newBrewer.Name);
    }

    [Theory]
    [InlineData("/Api/Brewer/{0}/UpdateBeer?beerId={1}")]
    public async Task UpdateBeer_ReturnsUpdatedBeer(string url)
    {
        var client = _factory.CreateClient();
        var brewer = await GetBrewer(client);

        var beers = await client.GetFromJsonAsync<List<BeerDto>>(string.Format("/Api/Brewer/{0}/Beers", brewer?.Id));
        var newBeer = new BeerCreateDto()
        {
            Name = "Modified",
            Description = "Modified",
            Flavor = "Modified",
            Price = 1
        };

        var response = await client.PutAsJsonAsync(string.Format(url, brewer?.Id, beers?[0].Id), newBeer);
        var created = await response.Content.ReadFromJsonAsync<BeerDto>();

        response.EnsureSuccessStatusCode();
        created.Should().NotBeNull();
        created?.Id.Should().NotBeNullOrWhiteSpace();
        created?.Name.Should().BeEquivalentTo(newBeer.Name);
    }

    private static async Task<BrewerDto?> GetBrewer(HttpClient client)
    {
        var breweries = await client.GetFromJsonAsync<List<BreweryDto>>("/Api/Brewery/All");
        var brewery = breweries?[0];

        var brewers = await client.GetFromJsonAsync<List<BrewerDto>>($"/Api/Brewery/{brewery?.Id}/Brewers");
        return brewers?[0];
    }
}
