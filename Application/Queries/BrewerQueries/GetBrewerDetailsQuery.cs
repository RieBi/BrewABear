namespace Application.Queries.BrewerQueries;
public record GetBrewerDetailsQuery(string BrewerId) : IRequest<BrewerDto>;
