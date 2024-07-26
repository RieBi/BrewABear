namespace Application.Commands.OrderCommands;
public record CreateOrderCommand(OrderCreateDto OrderCreateDto) : IRequest<OrderDto>;
