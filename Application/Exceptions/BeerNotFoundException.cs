namespace Application.Exceptions;
public class BeerNotFoundException(string beerId) : ResourceNotFoundException(beerId);
