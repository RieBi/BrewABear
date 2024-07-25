namespace Application.Exceptions;
public class ErrorDto(Exception exception, string message)
{
    public string Exception { get; set; } = exception.GetType().Name;
    public string Message { get; set; } = message;
}