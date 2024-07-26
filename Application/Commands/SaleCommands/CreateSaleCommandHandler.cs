using Application.Exceptions;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SaleCommands;
internal class CreateSaleCommandHandler(DataContext context, ISaleService saleService) : IRequestHandler<CreateSaleCommand, Unit>
{
    private readonly DataContext _context = context;
    private readonly ISaleService _saleService = saleService;

    public async Task<Unit> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        if (request.quantity < 0)
            throw new NegativeQuantityException(request.quantity);

        var wholesaler = await _context.Wholesalers
            .FindAsync([request.WholesalerId], cancellationToken: cancellationToken)
            ?? throw new WholesalerNotFoundException(request.WholesalerId);

        var beer = await _context.Beers
            .FindAsync([request.BeerId], cancellationToken: cancellationToken)
            ?? throw new BeerNotFoundException(request.BeerId);

        var inventories = await _context.WholesalerInventories
            .Where(f => f.WholesalerId == request.WholesalerId)
            .ToListAsync(cancellationToken);

        var item = _saleService.CreateSale(inventories, wholesaler, beer, request.quantity);
        if (item is not null)
            await _context.WholesalerInventories.AddAsync(item, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
