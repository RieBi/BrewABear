using Api.Services;
using Application.DTOs;
using Application.Exceptions;
using Application.Queries.WholesalerQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WholesalerController(IMediator mediator, IExceptionHandlerService exceptionHandler) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IExceptionHandlerService _exceptionHandler = exceptionHandler;

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
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }
}
