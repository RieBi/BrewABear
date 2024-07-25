using Application.Commands.BrewerCommands;
using Application.DTOs;
using Application.Exceptions;
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
    [ProducesResponseType<BrewerDto>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<IList<BeerDto>>> Beers(string id)
    {
        try
        {
            var beers = await _mediator.Send(new GetAllBeersByBrewerQuery(id));

            return Ok(beers);
        }
        catch (ResourceNotFoundException exception)
        {
            return CreateNotFoundResult(exception);
        }
    }

    [HttpPost]
    [Route("{id}/AddBeer")]
    [ProducesResponseType<BeerDto>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<BeerDto>> AddBeer(string id, BeerCreateDto beerCreateDto)
    {
        try
        {
            var newBeer = await _mediator.Send(new CreateBeerCommand(id, beerCreateDto));

            return Ok(newBeer);
        }
        catch (ResourceNotFoundException exception)
        {
            return CreateNotFoundResult(exception);
        }
    }

    [HttpPut]
    [Route("{id}/UpdateBeer")]
    public async Task<BeerDto?> UpdateBeer(string id, string beerId, BeerCreateDto beerCreateDto)
    {
        var updatedBeer = await _mediator.Send(new UpdateBeerCommand(id, beerId, beerCreateDto));

        return updatedBeer;
    }

    [HttpDelete]
    [Route("{id}/DeleteBeer")]
    public async Task<object?> DeleteBeer(string id, string beerId)
    {
        var obj = await _mediator.Send(new DeleteBeerCommand(id, beerId));

        return obj;
    }

    private NotFoundObjectResult CreateNotFoundResult(ResourceNotFoundException exception)
    {
        var message = $"Brewer with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }

    private NotFoundObjectResult CreateBeerNotFoundResult(BeerNotFoundException exception)
    {
        var message = $"Beer with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }
}
