namespace Application.Queries.WholesalerQueries;
public record GetWholesalerInventoryQuery(string WholesalerId) : IRequest<IList<WholesalerInventoryDto>>
