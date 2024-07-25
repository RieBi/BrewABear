namespace Application.DTOs;
public class WholesalerInventoryDto
{
    public string BeerId { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal FixedPrice { get; set; }
}
