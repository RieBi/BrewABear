using Application.Services;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.OrderQueries;
internal class GetAllOrdersQueryHandler(DataContext context, IMapper mapper, IOrderService orderService) : IRequestHandler<GetAllOrdersQuery, IList<OrderDto>>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderService _orderService = orderService;

    public async Task<IList<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (var order in orders)
            order.FinalPrice = _orderService.GetFinalPrice(order);

        return orders;
    }
}
