namespace Application.Exceptions;
public class NegativePriceException(decimal price) : BrewABeerException
{
    public decimal Price { get; set; } = price;
}
