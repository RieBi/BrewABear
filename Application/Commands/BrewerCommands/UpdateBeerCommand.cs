namespace Application.Commands.BrewerCommands;
public record UpdateBeerCommand(string BrewerId, string BeerId, BeerCreateDto Beer) : IRequest<BeerDto?>;
