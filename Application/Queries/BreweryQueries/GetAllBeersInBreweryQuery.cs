namespace Application.Queries.BreweryQueries;
public record GetAllBeersInBreweryQuery(string BreweryId) : IRequest<IList<BeerDto>?>;
