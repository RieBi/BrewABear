namespace Application.Commands.BrewerCommands;
public record DeleteBeerCommand(string BrewerId, string BeerId) : IRequest<Unit>;
