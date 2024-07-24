namespace Domain.Models;
public class Brewery
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;

    public IList<Brewer> Brewers { get; set; } = [];
    public IList<Beer> Beers { get; set; } = [];
}
