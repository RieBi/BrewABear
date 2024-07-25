using Application.Commands.SaleCommands;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SaleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult> Add(string wholesalerId, string beerId, int quantity)
    {
        try
        {
            await _mediator.Send(new CreateSaleCommand(wholesalerId, beerId, quantity));

            return Ok();
        }
        catch (WholesalerNotFoundException exception)
        {
            return CreateWholesalerNotFoundResult(exception);
        }
        catch (BeerNotFoundException exception)
        {
            return CreateBeerNotFoundResult(exception);
        }
    }

    private NotFoundObjectResult CreateWholesalerNotFoundResult(WholesalerNotFoundException exception)
    {
        var message = $"Wholesaler with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }

    private NotFoundObjectResult CreateBeerNotFoundResult(BeerNotFoundException exception)
    {
        var message = $"Beer with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }
}
