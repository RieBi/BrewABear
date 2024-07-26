namespace Application.Exceptions;
public class NegativeQuantityException(int quantity) : Exception
{
    public int Quantity { get; set; } = quantity;
}
