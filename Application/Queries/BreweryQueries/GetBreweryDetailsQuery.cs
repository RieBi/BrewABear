namespace Application.Queries.BreweryQueries;
public record GetBreweryDetailsQuery(string BreweryId) : IRequest<BreweryDto>;
