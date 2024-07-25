using Application.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WholesalerQueries;
internal class GetWholesalerInventoryQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetWholesalerInventoryQuery, IList<WholesalerInventoryDto>>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<WholesalerInventoryDto>> Handle(GetWholesalerInventoryQuery request, CancellationToken cancellationToken)
    {
        var count = await _context.Wholesalers
            .Where(f => f.Id == request.WholesalerId)
            .CountAsync(cancellationToken);

        if (count == 0)
            throw new ResourceNotFoundException(request.WholesalerId);

        IList<WholesalerInventoryDto> inventoryItems = await _context.WholesalerInventories
            .Where(f => f.WholesalerId == request.WholesalerId)
            .ProjectTo<WholesalerInventoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return inventoryItems;
    }
}
