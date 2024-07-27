using Application.DTOs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.ControllerTests;
public class BrewerControllerTests(ApiApplicationFactory factory) : IClassFixture<ApiApplicationFactory>
{
    private readonly ApiApplicationFactory _factory = factory;

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

    private static async Task<BrewerDto?> GetBrewer(HttpClient client)
    {
        var breweries = await client.GetFromJsonAsync<List<BreweryDto>>("/Api/Brewery/All");
        var brewery = breweries?[0];

        var brewers = await client.GetFromJsonAsync<List<BrewerDto>>($"/Api/Brewery/{brewery?.Id}/Brewers");
        return brewers?[0];
    }
}
