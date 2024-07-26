using Application.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.BreweryQueries;
internal class GetAllBrewersInBreweryQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetAllBrewersInBreweryQuery, IList<BrewerDto>?>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<BrewerDto>?> Handle(GetAllBrewersInBreweryQuery request, CancellationToken cancellationToken)
    {
        var count = await _context.Breweries
            .Where(f => f.Id == request.BreweryId)
            .CountAsync(cancellationToken);

        if (count == 0)
            throw new BreweryNotFoundException(request.BreweryId);

        IList<BrewerDto> brewers = await _context.Brewers
            .Where(f => f.Brewery.Id == request.BreweryId)
            .ProjectTo<BrewerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return brewers;
    }
}
