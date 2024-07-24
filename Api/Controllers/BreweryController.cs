using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BreweryController : ControllerBase
{
    [HttpGet]
    [Route("All")]
    [Route("")]
    public string All()
    {
        throw new NotImplementedException();
    }


}
