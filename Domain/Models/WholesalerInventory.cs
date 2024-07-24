namespace Domain.Models;
public class WholesalerInventory
{
    public Wholesaler Wholesaler { get; set; } = default!;
    public Beer Beer { get; set; } = default!;
    public int Quantity { get; set; }
}
