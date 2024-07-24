namespace Domain.Models;
public class Brewer
{
    public string Id { get; set; } = default!;
    public Brewery Brewery { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default;
    public string ContactEmail { get; set; } = default!;

    public IList<Beer> Beers { get; set; } = [];
}
