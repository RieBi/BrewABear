using Application.Commands.OrderCommands;
using Application.DTOs;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType<OrderDto>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<OrderDto>> Add(OrderCreateDto orderCreateDto)
    {
        try
        {
            var orderDto = await _mediator.Send(new CreateOrderCommand(orderCreateDto));

            return Ok(orderDto);
        }
        catch (WholesalerNotFoundException exception)
        {
            return CreateWholesalerNotFoundResult(exception);
        }
        catch (BeerNotFoundException exception)
        {
            return CreateBeerNotFoundResult(exception);
        }
        catch (NegativeQuantityException exception)
        {
            return CreateNegativeQuantityResult(exception);
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
        catch (OrderNotFoundException exception)
        {
            return CreateOrderNotFoundResult(exception);
        }
    }

    private NotFoundObjectResult CreateWholesalerNotFoundResult(WholesalerNotFoundException exception)
    {
        var message = $"Brewery with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }

    private NotFoundObjectResult CreateBeerNotFoundResult(BeerNotFoundException exception)
    {
        var message = $"Beer with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }

    private NotFoundObjectResult CreateOrderNotFoundResult(OrderNotFoundException exception)
    {
        var message = $"Order with id '{exception.ResourceId}' was not found.";
        return NotFound(new ErrorDto(exception, message));
    }

    private BadRequestObjectResult CreateNegativeQuantityResult(NegativeQuantityException exception)
    {
        var message = $"Quantity can't be less than 0. Was: {exception.Quantity}";
        return BadRequest(new ErrorDto(exception, message));
    }
}
