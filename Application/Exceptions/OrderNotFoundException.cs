namespace Application.Exceptions;
public class OrderNotFoundException(string orderId) : ResourceNotFoundException(orderId);
