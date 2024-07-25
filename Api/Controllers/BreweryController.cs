using Application.DTOs;
using Application.Queries.BreweryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BreweryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("All")]
    public async Task<IList<BreweryDto>> All()
    {
        var breweries = await _mediator.Send(new GetAllBreweriesQuery());

        return breweries;
    }

    [HttpGet]
    [Route("{id}/Beers")]
    public async Task<IList<BeerDto>?> Beers(string id)
    {
        var beers = await _mediator.Send(new GetAllBeersInBreweryQuery(id));

        return beers;
    }

    [HttpGet]
    [Route("{id}/Brewers")]
    public async Task<IList<BrewerDto>?> Brewers(string id)
    {
        var brewers = await _mediator.Send(new GetAllBrewersInBreweryQuery(id));

        return brewers;
    }
}
