namespace Application.DTOs;
public class BeerCreateDto
{
    public string Name { get; set; } = default!;
    public string Flavor { get; set; } = default!;
    public string? Description { get; set; } = default;
    public decimal Price { get; set; } = default;
}
