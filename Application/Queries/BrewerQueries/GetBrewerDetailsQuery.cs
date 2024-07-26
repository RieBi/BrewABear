namespace Application.Queries.BrewerQueries;
public record GetBrewerDetailsQuery(string brewerId) : IRequest<BrewerDto>;
