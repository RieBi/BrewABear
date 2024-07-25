using Application.Services;

namespace Application.Commands.BrewerCommands;
internal class CreateBeerCommandHandler(DataContext context, IMapper mapper, IGuidCreator creator) : IRequestHandler<CreateBeerCommand, BeerDto?>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IGuidCreator _creator = creator;

    public async Task<BeerDto?> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
    {
        var brewer =
            await _context.Brewers.FindAsync([request.BrewerId], cancellationToken: cancellationToken);

        if (brewer is null)
            return null;

        var newBeer = _mapper.Map<Beer>(request.Beer);
        newBeer.Brewer = brewer;
        newBeer.Id = _creator.Create();

        await _context.Beers.AddAsync(newBeer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BeerDto>(newBeer);
    }
}
