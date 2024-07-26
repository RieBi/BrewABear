using Application.Exceptions;
using Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.OrderCommands;
internal class CreateOrderCommandHandler(DataContext context, IMapper mapper, IOrderService orderService) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderService _orderService = orderService;

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.OrderCreateDto);
        if (order.Quantity < 0)
            throw new NegativeQuantityException(order.Quantity);

        var wholesalerCount = await _context.Wholesalers
            .Where(f => f.Id == order.WholesalerId)
            .CountAsync(cancellationToken);

        if (wholesalerCount < 1)
            throw new WholesalerNotFoundException(order.WholesalerId);

        var beer = await _context.Beers
            .FindAsync([order.BeerId], cancellationToken: cancellationToken)
            ?? throw new BeerNotFoundException(order.BeerId);

        order.PricePerBear = beer.Price;

        var dto = _mapper.Map<OrderDto>(order);
        dto.FinalPrice = _orderService.GetFinalPrice(order);

        return dto;
    }
}
