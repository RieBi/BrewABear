using AutoMapper.QueryableExtensions;

namespace Application.Queries.BreweryQueries;
internal class GetAllBreweriesQueryHandler(DataContext context, IMapper mapper) : IRequestHandler<GetAllBreweriesQuery, IList<BreweryDto>>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public Task<IList<BreweryDto>> Handle(GetAllBreweriesQuery request, CancellationToken cancellationToken)
    {
        IList<BreweryDto> breweries = [.. _context.Breweries.ProjectTo<BreweryDto>(_mapper.ConfigurationProvider)];

        return Task.FromResult(breweries);
    }
}
