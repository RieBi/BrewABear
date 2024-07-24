using Application.DTOs;
using Application.Queries.BreweryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BreweryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("All")]
    public async Task<IList<BreweryDto>> All()
    {
        var breweries = await _mediator.Send(new GetAllBreweriesQuery());

        return breweries;
    }


}
