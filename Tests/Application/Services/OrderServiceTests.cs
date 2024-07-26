using Application.Services;
using Domain.Models;

namespace Tests.Application.Services;
public class OrderServiceTests
{
    public static TheoryData<Order, decimal> GetOrderData() =>
        new()
        {
            { new() { PricePerBear = 5, Quantity = 10 }, 50 },
            { new() { PricePerBear = 2, Quantity = 5, DiscountPercentage = .5M }, 5 },
            { new() { PricePerBear = 100, Quantity = 100, DiscountPercentage = 1 }, 0 },
        };

    public static TheoryData<Order, decimal> GetQuotaData() =>
        new()
        {
            { new() { Quantity = 0 }, 0 },
            { new() { Quantity = 10 }, 0 },
            { new() { Quantity = 11 }, .1M },
            { new() { Quantity = 20 }, .1M },
            { new() { Quantity = 22 }, .2M },
            { new() { Quantity = 10000 }, .2M },
        };

    [Theory]
    [MemberData(nameof(GetOrderData))]
    public void GetsFinalPriceFromOrders(Order order, decimal expectedPrice)
    {
        var service = new OrderService();

        var actual = service.GetFinalPrice(order);

        Assert.Equal(expectedPrice, actual);
    }

    [Theory]
    [MemberData(nameof(GetQuotaData))]
    public void GetsQuotaPercentageFromOrders(Order order, decimal expectedQuota)
    {
        var service = new OrderService();

        var actual = service.GetQuotaPercentage(order);

        Assert.Equal(expectedQuota, actual);
    }
}
