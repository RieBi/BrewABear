using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.BreweryQueries;
internal class GetAllBeersInBreweryQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetAllBeersInBreweryQuery, IList<BeerDto>?>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<BeerDto>?> Handle(GetAllBeersInBreweryQuery request, CancellationToken cancellationToken)
    {
        var count = await _context.Breweries
            .Where(f => f.Id == request.BreweryId)
            .CountAsync(cancellationToken);

        if (count == 0)
            return null;

        var beers = await _context.Beers
            .Include(f => f.Brewer)
            .ThenInclude(f => f.Brewery)
            .Where(f => f.Brewer.Brewery.Id == request.BreweryId)
            .ProjectTo<BeerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (beers is null)
            return null;

        return _mapper.Map<IList<BeerDto>>(beers);
    }
}
