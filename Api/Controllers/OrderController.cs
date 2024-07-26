using Api.Services;
using Application.Commands.OrderCommands;
using Application.DTOs;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator, IExceptionHandlerService exceptionHandler) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IExceptionHandlerService _exceptionHandler = exceptionHandler;

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType<OrderDto>(200)]
    [ProducesResponseType<ErrorDto>(400)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<OrderDto>> Add(OrderCreateDto orderCreateDto)
    {
        try
        {
            var orderDto = await _mediator.Send(new CreateOrderCommand(orderCreateDto));

            return Ok(orderDto);
        }
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }

    [HttpPost]
    [Route("RequestQuote")]
    [ProducesResponseType<QuoteInfoDto>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<QuoteInfoDto>> RequestQuote(string orderId)
    {
        try
        {
            var info = await _mediator.Send(new RequestQuoteCommand(orderId));

            return Ok(info);
        }
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }
}
