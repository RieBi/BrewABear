namespace Application.Queries.BreweryQueries;
internal class GetAllBreweriesQueryHandler(DataContext context) : IRequestHandler<GetAllBreweriesQuery, IList<Brewery>>
{
    private readonly DataContext _context = context;

    public Task<IList<Brewery>> Handle(GetAllBreweriesQuery request, CancellationToken cancellationToken)
    {
        IList<Brewery> breweries = _context.Breweries.ToList();
        return Task.FromResult(breweries);
    }
}
