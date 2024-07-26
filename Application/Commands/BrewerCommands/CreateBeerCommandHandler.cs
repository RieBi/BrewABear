﻿using Application.Exceptions;
using Application.Services;

namespace Application.Commands.BrewerCommands;
internal class CreateBeerCommandHandler(DataContext context, IMapper mapper, IGuidCreator creator) : IRequestHandler<CreateBeerCommand, BeerDto>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IGuidCreator _creator = creator;

    public async Task<BeerDto> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
    {
        var brewer =
            await _context.Brewers.FindAsync([request.BrewerId], cancellationToken: cancellationToken)
            ?? throw new BrewerNotFoundException(request.BrewerId);

        var newBeer = _mapper.Map<Beer>(request.Beer);
        if (newBeer.Price < 0)
            throw new NegativePriceException(newBeer.Price);

        newBeer.Brewer = brewer;
        newBeer.Id = _creator.Create();

        await _context.Beers.AddAsync(newBeer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BeerDto>(newBeer);
    }
}
