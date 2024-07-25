using Application.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.BrewerQueries;
internal class GetAllBeersByBrewerQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetAllBeersByBrewerQuery, IList<BeerDto>>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<BeerDto>> Handle(GetAllBeersByBrewerQuery request, CancellationToken cancellationToken)
    {
        var count = await _context.Brewers
            .Where(f => f.Id == request.BrewerId)
            .CountAsync(cancellationToken);

        if (count == 0)
            throw new ResourceNotFoundException(request.BrewerId);

        IList<BeerDto> beers = await _context.Beers
            .Include(f => f.Brewer)
            .Where(f => f.Brewer.Id == request.BrewerId)
            .ProjectTo<BeerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return beers;
    }
}
