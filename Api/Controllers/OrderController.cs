﻿using Api.Services;
using Application.Commands.OrderCommands;
using Application.DTOs;
using Application.Exceptions;
using Application.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator, IExceptionHandlerService exceptionHandler) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IExceptionHandlerService _exceptionHandler = exceptionHandler;

    [HttpGet]
    [Route("All")]
    [ProducesResponseType<IList<OrderDto>>(200)]
    public async Task<ActionResult<IList<OrderDto>>> All()
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery());

        return Ok(orders);
    }

    [HttpGet]
    [Route("{id}/Details")]
    [ProducesResponseType<OrderDto>(200)]
    [ProducesResponseType<ErrorDto>(404)]
    public async Task<ActionResult<OrderDto>> Details(string id)
    {
        try
        {
            var order = await _mediator.Send(new GetOrderDetailsQuery(id));

            return Ok(order);
        }
        catch (Exception ex)
        {
            return _exceptionHandler.HandleException(ex);
        }
    }

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
