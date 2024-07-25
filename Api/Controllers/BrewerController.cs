using Application.Commands.BrewerCommands;
using Application.DTOs;
using Application.Queries.BrewerQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BrewerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("{id}/Beers")]
    public async Task<IList<BeerDto>?> Beers(string id)
    {
        var beers = await _mediator.Send(new GetAllBeersByBrewerQuery(id));

        return beers;
    }

    [HttpPost]
    [Route("{id}/AddBeer")]
    public async Task<BeerDto?> AddBeer(string id, BeerCreateDto beerCreateDto)
    {
        var newBeer = await _mediator.Send(new CreateBeerCommand(id, beerCreateDto));

        return newBeer;
    }

    [HttpPut]
    [Route("{id}/UpdateBeer")]
    public async Task<BeerDto?> UpdateBeer(string id, string beerId, BeerCreateDto beerCreateDto)
    {
        var updatedBeer = await _mediator.Send(new UpdateBeerCommand(id, beerId, beerCreateDto));

        return updatedBeer;
    }
}
