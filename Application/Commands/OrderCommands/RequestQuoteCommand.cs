namespace Application.Commands.OrderCommands;
public record RequestQuoteCommand(string OrderId) : IRequest<QuoteInfoDto>;
