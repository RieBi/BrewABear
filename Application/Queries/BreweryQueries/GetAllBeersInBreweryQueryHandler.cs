using Application.Exceptions;
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
            throw new ResourceNotFoundException(request.BreweryId);

        IList<BeerDto> beers = await _context.Beers
            .Include(f => f.Brewer)
            .ThenInclude(f => f.Brewery)
            .Where(f => f.Brewer.Brewery.Id == request.BreweryId)
            .ProjectTo<BeerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return beers;
    }
}
