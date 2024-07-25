namespace Application.DTOs;
public class WholesalerInventoryDto
{
    public string BeerId { get; set; } = default!;
    public int Quantity { get; set; }
    public int FixedPrice { get; set; }
}
