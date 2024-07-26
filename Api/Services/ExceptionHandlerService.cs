using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class ExceptionHandlerService : IExceptionHandlerService
{
    public ActionResult HandleException(Exception exception)
    {
        return exception switch
        {
            BeerNotFoundException ex => CreateBeerNotFoundResult(ex),
            WholesalerNotFoundException ex => CreateWholesalerNotFoundResult(ex),
            OrderNotFoundException ex => CreateOrderNotFoundResult(ex),
            ResourceNotFoundException ex => CreateNotFoundResult(ex),
            NegativePriceException ex => CreateNegativePriceResult(ex),
            NegativeQuantityException ex => CreateNegativeQuantityResult(ex),
            _ => CreateGenericErrorResult(exception)
        };
    }

    private static NotFoundObjectResult CreateNotFoundResult(ResourceNotFoundException exception)
    {
        var message = $"Resource with id '{exception.ResourceId}' was not found.";
        return new(new ErrorDto(exception, message));
    }

    private static NotFoundObjectResult CreateBeerNotFoundResult(BeerNotFoundException exception)
    {
        var message = $"Beer with id '{exception.ResourceId}' was not found.";
        return new(new ErrorDto(exception, message));
    }

    private static NotFoundObjectResult CreateWholesalerNotFoundResult(WholesalerNotFoundException exception)
    {
        var message = $"Wholesaler with id '{exception.ResourceId}' was not found.";
        return new(new ErrorDto(exception, message));
    }

    private static NotFoundObjectResult CreateOrderNotFoundResult(OrderNotFoundException exception)
    {
        var message = $"Order with id '{exception.ResourceId}' was not found.";
        return new(new ErrorDto(exception, message));
    }

    private static BadRequestObjectResult CreateNegativePriceResult(NegativePriceException exception)
    {
        var message = $"Price can't be less than 0. Was: {exception.Price}";
        return new(new ErrorDto(exception, message));
    }

    private static BadRequestObjectResult CreateNegativeQuantityResult(NegativeQuantityException exception)
    {
        var message = $"Quantity can't be less than 0. Was: {exception.Quantity}";
        return new(new ErrorDto(exception, message));
    }

    private static ObjectResult CreateGenericErrorResult(Exception exception)
    {
        var message = "An unexpected error occurred.";
        return new ObjectResult(new ErrorDto(exception, message))
        {
            StatusCode = 500
        };
    }
}
