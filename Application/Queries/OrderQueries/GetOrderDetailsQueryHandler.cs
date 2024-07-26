using Application.Exceptions;
using Application.Services;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.OrderQueries;
internal class GetOrderDetailsQueryHandler(DataContext context, IMapper mapper, IOrderService orderService) : IRequestHandler<GetOrderDetailsQuery, OrderDto>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderService _orderService = orderService;

    public async Task<OrderDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Where(f => f.Id == request.OrderId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new OrderNotFoundException(request.OrderId);

        order.FinalPrice = _orderService.GetFinalPrice(order);

        return order;
    }
}
