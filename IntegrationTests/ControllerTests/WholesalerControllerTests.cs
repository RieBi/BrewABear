using Application.DTOs;
using FluentAssertions;
using System.Net.Http.Json;

namespace IntegrationTests.ControllerTests;
public class WholesalerControllerTests(ApiApplicationFactory factory) : IClassFixture<ApiApplicationFactory>
{
    private readonly ApiApplicationFactory _factory = factory;

    [Theory]
    [InlineData("/Api/Wholesaler/All", "/Api/Wholesaler/{0}/Details", "/Api/Wholesaler/{0}/Inventory")]
    public async Task WholesalerEndpoints_ReturnInformation(string url1, string url2, string url3)
    {
        var client = _factory.CreateClient();

        var wholesalers = await client.GetFromJsonAsync<List<WholesalerDto>>(url1);

        wholesalers.Should().NotBeNullOrEmpty();

        var saler = wholesalers?[0];
        saler.Should().NotBeNull();

        var detailed = await client.GetFromJsonAsync<WholesalerDto>(string.Format(url2, saler?.Id));
        detailed.Should().NotBeNull();
        detailed?.Id.Should().BeEquivalentTo(saler?.Id);
        detailed?.Name.Should().BeEquivalentTo(saler?.Name);

        var inventory = await client.GetFromJsonAsync<List<WholesalerInventoryDto>>(string.Format(url3, detailed?.Id));
        inventory.Should().NotBeNullOrEmpty();
        inventory?[0].BeerId.Should().NotBeNullOrWhiteSpace();
        inventory?[0].Quantity.Should().BeGreaterThan(1);
    }
}
