namespace Application.Queries.BreweryQueries;
public record GetAllBrewersInBreweryQuery(string BreweryId) : IRequest<IList<BrewerDto>?>;
