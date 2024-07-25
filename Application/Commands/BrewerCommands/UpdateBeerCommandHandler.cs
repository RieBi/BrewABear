namespace Application.Commands.BrewerCommands;
internal class UpdateBeerCommandHandler(DataContext context, IMapper mapper) : IRequestHandler<UpdateBeerCommand, BeerDto?>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<BeerDto?> Handle(UpdateBeerCommand request, CancellationToken cancellationToken)
    {
        var beer =
            await _context.Beers.FindAsync([request.BeerId], cancellationToken: cancellationToken);

        if (beer is null)
            return null;

        if (beer.BrewerId != request.BrewerId)
            return null;

        beer.Name = request.Beer.Name;
        beer.Flavor = request.Beer.Flavor;
        beer.Description = request.Beer.Description;
        beer.Price = request.Beer.Price;

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<BeerDto>(beer);
    }
}
