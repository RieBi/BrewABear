namespace Application.Exceptions;
public class WholesalerNotFoundException(string wholesalerId) : ResourceNotFoundException(wholesalerId);
