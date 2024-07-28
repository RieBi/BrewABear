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
}
