using Application.DTOs;
using Application.Exceptions;
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
    [ProducesResponseType<BeerDto>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<IList<BeerDto>>> Beers(string id)
    {
        try
        {
            var beers = await _mediator.Send(new GetAllBeersInBreweryQuery(id));

            return Ok(beers);
        }
        catch (ResourceNotFoundException exception)
        {
            return CreateNotFoundResult(exception);
        }
    }

    [HttpGet]
    [Route("{id}/Brewers")]
    [ProducesResponseType<BrewerDto>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<IList<BrewerDto>>> Brewers(string id)
    {
        try
        {
            var brewers = await _mediator.Send(new GetAllBrewersInBreweryQuery(id));

            return Ok(brewers);
        }
        catch (ResourceNotFoundException exception)
        {
            return CreateNotFoundResult(exception);
        }
    }

    private NotFoundObjectResult CreateNotFoundResult(ResourceNotFoundException exception)
    {
        var message = $"Brewery with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }
}
