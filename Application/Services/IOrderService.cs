
namespace Application.Services;

public interface IOrderService
{
    decimal GetFinalPrice(Order order);
    decimal GetFinalPrice(OrderDto order);
    decimal GetQuotaPercentage(Order order);
}