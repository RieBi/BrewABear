using Application.DTOs;
using Application.Exceptions;
using Application.Queries.WholesalerQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WholesalerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("All")]
    [ProducesResponseType<IList<WholesalerDto>>(200)]
    public async Task<ActionResult<IList<WholesalerDto>>> All()
    {
        var wholesalers = await _mediator.Send(new GetAllWholesalersQuery());

        return Ok(wholesalers);
    }

    [HttpGet]
    [Route("{id}/Inventory")]
    [ProducesResponseType<IList<WholesalerDto>>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<IList<WholesalerDto>>> Inventory(string id)
    {
        try
        {
            var wholesalerInventory = await _mediator.Send(new GetWholesalerInventoryQuery(id));

            return Ok(wholesalerInventory);
        }
        catch (ResourceNotFoundException exception)
        {
            return CreateNotFoundResult(exception);
        }
    }

    private NotFoundObjectResult CreateNotFoundResult(ResourceNotFoundException exception)
    {
        var message = $"Wholesaler with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }
}
