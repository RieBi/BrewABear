using Application.Queries.BreweryQueries;
using Domain.Models;
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
    [Route("")]
    public async Task<IList<Brewery>> All()
    {
        var breweries = await _mediator.Send(new GetAllBreweriesQuery());

        return breweries;
    }


}
