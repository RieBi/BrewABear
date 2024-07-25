namespace Application.Commands.BrewerCommands;
internal class DeleteBeerCommandHandler(DataContext context) : IRequestHandler<DeleteBeerCommand, object?>
{
    private readonly DataContext _context = context;

    public async Task<object?> Handle(DeleteBeerCommand request, CancellationToken cancellationToken)
    {
        var beer =
            await _context.Beers.FindAsync([request.BeerId], cancellationToken: cancellationToken);

        if (beer is null)
            return null;

        if (beer.BrewerId != request.BrewerId)
            return null;

        _context.Beers.Remove(beer);
        await _context.SaveChangesAsync(cancellationToken);

        return new();
    }
}
