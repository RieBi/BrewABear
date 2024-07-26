namespace Application.Exceptions;
public class BreweryNotFoundException(string breweryId) : ResourceNotFoundException(breweryId);
