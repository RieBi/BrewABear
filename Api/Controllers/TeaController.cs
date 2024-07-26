using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeaController : ControllerBase
{
    [HttpGet]
    [Route("GetTea")]
    [ProducesResponseType<string>(200)]
    public string GetTea() => teaArt;

    [HttpGet]
    [Route("GetCoffee")]
    [ProducesResponseType<ErrorDto>(418)]
    public ActionResult GetCoffee()
    {
        var error = new ErrorDto(new PermanentlyTeapotException(), "I'm a teapot, not a coffemaker, permanently now.");
        return new ObjectResult(error)
        {
            StatusCode = 418
        };
    }

    private static readonly string teaArt = """
                     ;,'
             _o_    ;:;'
         ,-.'---`.__ ;
        ((j`=====',-'
         `-\     /
            `-=-'
        
        """;
}
