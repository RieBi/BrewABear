using Api.Services;
using Application.Commands.SaleCommands;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SaleController(IMediator mediator, IExceptionHandlerService exceptionHandler) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IExceptionHandlerService _exceptionHandler = exceptionHandler;

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(200)]
    [ProducesResponseType<ErrorDto>(400)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult> Add(string wholesalerId, string beerId, int quantity)
    {
        try
        {
            await _mediator.Send(new CreateSaleCommand(wholesalerId, beerId, quantity));

            return Ok();
        }
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }
}
