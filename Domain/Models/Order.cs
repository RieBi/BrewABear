namespace Domain.Models;
public class Order
{
    public string Id { get; set; } = default!;
    public string ClientEmail { get; set; } = default!;
    public Beer Beer { get; set; } = default!;
    public Wholesaler Wholesaler { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal PricePerBear { get; set; }
    public decimal DiscountPercentage { get; set; }
}
