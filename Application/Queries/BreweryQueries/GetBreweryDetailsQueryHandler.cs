using Application.Exceptions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.BreweryQueries;
internal class GetBreweryDetailsQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetBreweryDetailsQuery, BreweryDto>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<BreweryDto> Handle(GetBreweryDetailsQuery request, CancellationToken cancellationToken)
    {
        var brewery = await _context.Breweries
            .Where(f => f.Id == request.BreweryId)
            .ProjectTo<BreweryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new BreweryNotFoundException(request.BreweryId);

        return brewery;
    }
}
