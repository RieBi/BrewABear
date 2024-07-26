using Api.Services;
using Application.Commands.BrewerCommands;
using Application.DTOs;
using Application.Exceptions;
using Application.Queries.BrewerQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BrewerController(IMediator mediator, IExceptionHandlerService exceptionHandler) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IExceptionHandlerService _exceptionHandler = exceptionHandler;

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
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }

    [HttpPost]
    [Route("{id}/AddBeer")]
    [ProducesResponseType<BeerDto>(200)]
    [ProducesResponseType<ErrorDto>(400)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<BeerDto>> AddBeer(string id, BeerCreateDto beerCreateDto)
    {
        try
        {
            var newBeer = await _mediator.Send(new CreateBeerCommand(id, beerCreateDto));

            return Ok(newBeer);
        }
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }

    [HttpPut]
    [Route("{id}/UpdateBeer")]
    [ProducesResponseType<BeerDto>(200)]
    [ProducesResponseType<ErrorDto>(400)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<BeerDto>> UpdateBeer(string id, string beerId, BeerCreateDto beerCreateDto)
    {
        try
        {
            var updatedBeer = await _mediator.Send(new UpdateBeerCommand(id, beerId, beerCreateDto));

            return Ok(updatedBeer);
        }
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }

    [HttpDelete]
    [Route("{id}/DeleteBeer")]
    [ProducesResponseType(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult> DeleteBeer(string id, string beerId)
    {
        try
        {
            await _mediator.Send(new DeleteBeerCommand(id, beerId));

            return Ok();
        }
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }
}
