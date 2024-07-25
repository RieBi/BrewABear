using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WholesalerQueries;
internal class GetAllWholesalersQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetAllWholesalersQuery, IList<WholesalerDto>>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<WholesalerDto>> Handle(GetAllWholesalersQuery request, CancellationToken cancellationToken)
    {
        IList<WholesalerDto> wholesalers = await _context.Wholesalers
            .ProjectTo<WholesalerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return wholesalers;
    }
}
