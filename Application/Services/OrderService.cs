namespace Application.Services;
public class OrderService : IOrderService
{
    public decimal GetFinalPrice(Order order) => order.Quantity * order.PricePerBear;

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
