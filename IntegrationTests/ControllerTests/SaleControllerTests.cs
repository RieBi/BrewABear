using Application.DTOs;
using FluentAssertions;
using System.Net.Http.Json;

namespace IntegrationTests.ControllerTests;
public class SaleControllerTests(ApiApplicationFactory factory) : IClassFixture<ApiApplicationFactory>
{
    private readonly ApiApplicationFactory _factory = factory;

    [Theory]
    [InlineData(
        "/Api/Wholesaler/All",
        "/Api/Brewery/All",
        "/Api/Brewery/{0}/Beers",
        "/Api/Sale/Add?wholesalerId={0}&beerId={1}&quantity=10")]
    public async Task SaleEndpoint_ReturnsSuccess(string url1, string url2, string url3, string url4)
    {
        var client = _factory.CreateClient();

        var wholesalers = await client.GetFromJsonAsync<List<WholesalerDto>>(url1);
        var saler = wholesalers?[0];

        var breweries = await client.GetFromJsonAsync<List<BreweryDto>>(url2);
        var brewery = breweries?[0];

        var beers = await client.GetFromJsonAsync<List<BeerDto>>(string.Format(url3, brewery?.Id));
        var beer = beers?[0];

        var content = new StringContent(string.Empty);
        var response = await client.PostAsync(string.Format(url4, saler?.Id, beer?.Id), content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadAsStringAsync()).Should().BeNullOrWhiteSpace();
    }
}
