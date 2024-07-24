namespace Application.DTOs;
public class BrewerDto
{
    public string Id { get; set; } = default!;
    public string BreweryId { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default;
    public string ContactEmail { get; set; } = default!;
}
