using Application.DTOs;
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
}
