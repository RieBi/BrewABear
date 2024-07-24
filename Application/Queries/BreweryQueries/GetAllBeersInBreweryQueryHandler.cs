using Microsoft.EntityFrameworkCore;

namespace Application.Queries.BreweryQueries;
internal class GetAllBeersInBreweryQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetAllBeersInBreweryQuery, IList<BeerDto>?>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IList<BeerDto>?> Handle(GetAllBeersInBreweryQuery request, CancellationToken cancellationToken)
    {
        var beers = await _context.Breweries
            .Include(f => f.Beers)
            .Where(f => f.Id == request.BreweryId)
            .Select(f => f.Beers)
            .SingleOrDefaultAsync(cancellationToken);

        if (beers is null)
            return null;

        return _mapper.Map<IList<BeerDto>>(beers);
    }
}
