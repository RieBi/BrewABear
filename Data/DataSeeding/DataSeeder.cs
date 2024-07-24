using Domain.Models;

namespace Data.DataSeeding;
public class DataSeeder(DataContext context)
{
    private readonly DataContext _context = context;

    public void ApplySeeding()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        IList<Brewery> breweries =
        [
            new()
            {
                Id = CreateGuid(),
                Name = "Bear Brewery",
                Address = "Prague, Big Street, 752/13",
            },
            new()
            {
                Id = CreateGuid(),
                Name = "Alien Hot Brewery",
                Address = "Mars, Small Crater, Loophole 17, 1",
            }
        ];

        _context.Breweries.AddRange(breweries);

        IList<Brewer> brewers =
        [
            new()
            {
                Id = CreateGuid(),
                Brewery = breweries[0],
                FirstName = "John",
                LastName = "Wick",
                ContactEmail = "john.superwick@superjohnwick.example.org"
            },
            new()
            {
                Id = CreateGuid(),
                Brewery = breweries[1],
                FirstName = "Pablo",
                LastName = "Biden",
                ContactEmail = "pablo1234qwerty@biden.example.org"
            },
        ];

        _context.Brewers.AddRange(brewers);

        _context.SaveChanges();
    }

    private string CreateGuid() => Guid.NewGuid().ToString();
}
