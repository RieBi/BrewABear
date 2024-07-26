namespace Application.Exceptions;
public class NegativeQuantityException(int quantity) : BrewABeerException
{
    public int Quantity { get; set; } = quantity;
}
