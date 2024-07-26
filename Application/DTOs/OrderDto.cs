namespace Application.DTOs;
public class OrderDto
{
    public string Id { get; set; } = default!;
    public string ClientEmail { get; set; } = default!;
    public string BeerId { get; set; } = default!;
    public string WholesalerId { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal PricePerBear { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal FinalPrice { get; set; }
}
