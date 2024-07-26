using Application.Exceptions;

namespace Application.Commands.BrewerCommands;
internal class DeleteBeerCommandHandler(DataContext context) : IRequestHandler<DeleteBeerCommand, Unit>
{
    private readonly DataContext _context = context;

    public async Task<Unit> Handle(DeleteBeerCommand request, CancellationToken cancellationToken)
    {
        var beer =
            await _context.Beers.FindAsync([request.BeerId], cancellationToken: cancellationToken)
            ?? throw new BeerNotFoundException(request.BeerId);

        if (beer.BrewerId != request.BrewerId)
            throw new BrewerNotFoundException(request.BrewerId);

        _context.Beers.Remove(beer);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
