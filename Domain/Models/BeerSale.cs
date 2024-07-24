namespace Domain.Models;
public class BeerSale
{
    public string Id { get; set; } = default!;
    public Beer Beer { get; set; } = default!;
    public Wholesaler Wholesaler { get; set; } = default!;
    public int Quantity { get; set; }
}
