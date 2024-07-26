namespace Application.Exceptions;
public class BrewerNotFoundException(string brewerId) : ResourceNotFoundException(brewerId);
