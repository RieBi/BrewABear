namespace Application.DTOs;
public class OrderCreateDto
{
    public string ClientEmail { get; set; } = default!;
    public string BeerId { get; set; } = default!;
    public string WholesalerId { get; set; } = default!;
    public int Quantity { get; set; }
}
