namespace Application.Queries.OrderQueries;
public record GetAllOrdersQuery : IRequest<IList<OrderDto>>;
