using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.OrderQueries;
internal class GetAllOrdersQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, IList<OrderDto>>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return orders;
    }
}
