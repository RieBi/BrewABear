namespace Application.Services;
public class OrderService : IOrderService
{
    public decimal GetFinalPrice(Order order) => order.Quantity * order.PricePerBear * (1 - order.DiscountPercentage);
    public decimal GetFinalPrice(OrderDto order) => order.Quantity * order.PricePerBear * (1 - order.DiscountPercentage);

    public decimal GetQuotaPercentage(Order order)
    {
        return order.Quantity switch
        {
            > 20 => .2M,
            > 10 => .1M,
            _ => 0
        };
    }
}
