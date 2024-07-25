namespace Application.Exceptions;
public class ResourceNotFoundException(string resourceId) : Exception
{
    public string ResourceId { get; set; } = resourceId;
}
