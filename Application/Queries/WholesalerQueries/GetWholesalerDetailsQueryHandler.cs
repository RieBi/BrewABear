using Application.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WholesalerQueries;
internal class GetWholesalerDetailsQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetWholesalerDetailsQuery, WholesalerDto>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<WholesalerDto> Handle(GetWholesalerDetailsQuery request, CancellationToken cancellationToken)
    {
        var wholesaler = await _context.Wholesalers
            .Where(f => f.Id == request.WholesalerId)
            .ProjectTo<WholesalerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new WholesalerNotFoundException(request.WholesalerId);

        return wholesaler;
    }
}
