namespace Application.Queries.OrderQueries;
public record GetOrderDetailsQuery(string OrderId) : IRequest<OrderDto>;
