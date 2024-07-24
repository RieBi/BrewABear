namespace Domain.Models;
public class Beer
{
    public string Id { get; set; } = default!;
    public Brewer Brewer { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Flavor { get; set; } = default!;
    public string? Description { get; set; } = default;
    public decimal Price { get; set; } = default;
}
