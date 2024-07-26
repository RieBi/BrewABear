using Application.Exceptions;
using Application.Services;

namespace Application.Commands.OrderCommands;
internal class RequestQuoteCommandHandler(DataContext context, IOrderService orderService) : IRequestHandler<RequestQuoteCommand, QuoteInfoDto>
{
    private readonly DataContext _context = context;
    private readonly IOrderService _orderService = orderService;

    public async Task<QuoteInfoDto> Handle(RequestQuoteCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FindAsync([request.OrderId], cancellationToken: cancellationToken)
            ?? throw new OrderNotFoundException(request.OrderId);

        order.DiscountPercentage = _orderService.GetQuotaPercentage(order);

        await _context.SaveChangesAsync(cancellationToken);

        var info = new QuoteInfoDto()
        {
            IsSuccessful = order.DiscountPercentage > 0,
            OrderId = request.OrderId,
            QuotePercentage = order.DiscountPercentage,
            NewTotalPrice = _orderService.GetFinalPrice(order)
        };

        return info;
    }
}
