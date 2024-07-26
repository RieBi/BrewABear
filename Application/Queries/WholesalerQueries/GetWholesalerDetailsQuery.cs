namespace Application.Queries.WholesalerQueries;
public record GetWholesalerDetailsQuery(string WholesalerId) : IRequest<WholesalerDto>;
