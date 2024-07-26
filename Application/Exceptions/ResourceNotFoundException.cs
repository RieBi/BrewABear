namespace Application.Exceptions;
public class ResourceNotFoundException(string resourceId) : BrewABeerException
{
    public string ResourceId { get; set; } = resourceId;
}
