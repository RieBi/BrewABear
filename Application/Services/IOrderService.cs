
namespace Application.Services;

public interface IOrderService
{
    decimal GetFinalPrice(Order order);
    decimal GetQuotaPercentage(Order order);
}