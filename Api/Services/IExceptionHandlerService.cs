using Microsoft.AspNetCore.Mvc;

namespace Api.Services;
public interface IExceptionHandlerService
{
    ActionResult HandleException(Exception exception);
}