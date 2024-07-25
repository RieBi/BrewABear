namespace Application.Queries.BrewerQueries;
public record GetAllBeersByBrewerQuery(string BrewerId) : IRequest<IList<BeerDto>>;
