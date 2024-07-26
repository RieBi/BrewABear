using Application.Exceptions;

namespace Application.Commands.BrewerCommands;
internal class UpdateBeerCommandHandler(DataContext context, IMapper mapper) : IRequestHandler<UpdateBeerCommand, BeerDto>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<BeerDto> Handle(UpdateBeerCommand request, CancellationToken cancellationToken)
    {
        var beer =
            await _context.Beers.FindAsync([request.BeerId], cancellationToken: cancellationToken)
            ?? throw new BeerNotFoundException(request.BeerId);

        if (beer.BrewerId != request.BrewerId)
            throw new BrewerNotFoundException(request.BrewerId);

        if (request.Beer.Price < 0)
            throw new NegativePriceException(request.Beer.Price);

        beer.Name = request.Beer.Name;
        beer.Flavor = request.Beer.Flavor;
        beer.Description = request.Beer.Description;
        beer.Price = request.Beer.Price;

        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<BeerDto>(beer);
    }
}
