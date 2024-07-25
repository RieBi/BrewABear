namespace Application.Commands.SaleCommands;
public record CreateSaleCommand(string WholesalerId, string BeerId, int quantity) : IRequest<Unit>;
