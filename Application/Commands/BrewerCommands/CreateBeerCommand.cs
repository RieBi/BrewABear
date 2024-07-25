namespace Application.Commands.BrewerCommands;
public record CreateBeerCommand(string BrewerId, BeerCreateDto Beer) : IRequest<BeerDto>;
