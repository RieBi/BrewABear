using Application.DTOs;
using FluentAssertions;
using System.Net.Http.Json;

namespace IntegrationTests.ControllerTests;
public class OrderControllerTests(ApiApplicationFactory factory) : IClassFixture<ApiApplicationFactory>
{
    private readonly ApiApplicationFactory _factory = factory;

    [Theory]
    [InlineData("/Api/Order/All", "/Api/Order/{0}/Details")]
    public async Task GetOrderEndpoints_ReturnOrders(string url1, string url2)
    {
        var client = _factory.CreateClient();

        var orders = await client.GetFromJsonAsync<List<OrderDto>>(url1);

        orders.Should().NotBeNullOrEmpty();
        var order = orders?[0];
        order.Should().NotBeNull();
        order?.Id.Should().NotBeNullOrWhiteSpace();
        order?.ClientEmail.Should().NotBeNullOrWhiteSpace();

        var orderDetailed = await client.GetFromJsonAsync<OrderDto>(string.Format(url2, order?.Id));

        orderDetailed.Should().NotBeNull();
        orderDetailed.Should().BeEquivalentTo(order);
    }

    [Theory]
    [InlineData(
        "/Api/Wholesaler/All",
        "/Api/Brewery/All",
        "/Api/Brewery/{0}/Beers",
        "/Api/Order/Add",
        "/Api/Order/RequestQuote?orderid={0}")]
    public async Task PostOrderEndpoints_ReturnData(string url1, string url2, string url3, string url4, string url5)
    {
        var client = _factory.CreateClient();

        var wholesalers = await client.GetFromJsonAsync<List<WholesalerDto>>(url1);
        var saler = wholesalers?[0];

        var breweries = await client.GetFromJsonAsync<List<BreweryDto>>(url2);
        var brewery = breweries?[0];

        var beers = await client.GetFromJsonAsync<List<BeerDto>>(string.Format(url3, brewery?.Id));
        var beer = beers?[0];

        saler.Should().NotBeNull();
        beer.Should().NotBeNull();

        var order1 = new OrderCreateDto()
        {
            ClientEmail = "client1@example.org",
            BeerId = beer!.Id,
            WholesalerId = saler!.Id,
            Quantity = 7
        };

        var response1 = await client.PostAsJsonAsync(url4, order1);
        response1.EnsureSuccessStatusCode();
        var result1 = await response1.Content.ReadFromJsonAsync<OrderDto>();

        result1.Should().NotBeNull();
        result1?.ClientEmail.Should().BeEquivalentTo(order1.ClientEmail);
        result1?.BeerId.Should().BeEquivalentTo(order1.BeerId);
        result1?.WholesalerId.Should().BeEquivalentTo(order1.WholesalerId);
        result1?.Quantity.Should().Be(order1.Quantity);

        var order2 = new OrderCreateDto()
        {
            ClientEmail = "client2@example.org",
            BeerId = beer!.Id,
            WholesalerId = saler!.Id,
            Quantity = 77
        };

        var response2 = await client.PostAsJsonAsync(url4, order2);
        response2.EnsureSuccessStatusCode();
        var result2 = await response2.Content.ReadFromJsonAsync<OrderDto>();

        result2.Should().NotBeNull();
        result2?.ClientEmail.Should().BeEquivalentTo(order2.ClientEmail);
        result2?.BeerId.Should().BeEquivalentTo(order2.BeerId);
        result2?.WholesalerId.Should().BeEquivalentTo(order2.WholesalerId);
        result2?.Quantity.Should().Be(order2.Quantity);

        var quoteResponse1 = await client.PostAsJsonAsync(string.Format(url5, result1?.Id), new StringContent(string.Empty));
        quoteResponse1.EnsureSuccessStatusCode();
        var quoteResult1 = await quoteResponse1.Content.ReadFromJsonAsync<QuoteInfoDto>();

        quoteResult1.Should().NotBeNull();
        quoteResult1?.IsSuccessful.Should().BeFalse();
        quoteResult1?.OrderId.Should().BeEquivalentTo(result1?.Id);

        var quoteResponse2 = await client.PostAsJsonAsync(string.Format(url5, result2?.Id), new StringContent(string.Empty));
        quoteResponse2.EnsureSuccessStatusCode();
        var quoteResult2 = await quoteResponse2.Content.ReadFromJsonAsync<QuoteInfoDto>();

        quoteResult2.Should().NotBeNull();
        quoteResult2?.IsSuccessful.Should().BeTrue();
        quoteResult2?.OrderId.Should().BeEquivalentTo(result2?.Id);
    }
}
