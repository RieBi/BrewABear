namespace Application.Exceptions;
public class NegativePriceException(decimal price) : Exception
{
    public decimal Price { get; set; } = price;
}
