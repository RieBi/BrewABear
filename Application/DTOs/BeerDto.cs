namespace Application.DTOs;
internal class BeerDto
{
    public string Id { get; set; } = default!;
    public string BrewerId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Flavor { get; set; } = default!;
    public string? Description { get; set; } = default;
    public decimal Price { get; set; } = default;
}
