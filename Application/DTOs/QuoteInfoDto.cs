namespace Application.DTOs;
public class QuoteInfoDto
{
    public bool IsSuccessful { get; set; }
    public string OrderId { get; set; } = default!;
    public decimal QuotePercentage { get; set; }
    public decimal NewTotalPrice { get; set; }
}
