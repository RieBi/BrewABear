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

        IList<Beer> beers =
        [
            new()
            {
                Id = CreateGuid(),
                Brewer = brewers[0],
                Name = "Quaternibeer",
                Description = "Juicy beer, made for you.",
                Flavor = "Moustache grape",
                Price = 100,
            },
            new()
            {
                Id = CreateGuid(),
                Brewer = brewers[0],
                Name = "Bolsoyam",
                Description = "Bean beer for Mr. Bean.",
                Flavor = "Quirky apathy",
                Price = 77,
            },
            new()
            {
                Id = CreateGuid(),
                Brewer = brewers[1],
                Name = "Big beer",
                Description = "All you need for a day",
                Flavor = "Strawberries",
                Price = 222,
            },
            new()
            {
                Id = CreateGuid(),
                Brewer = brewers[1],
                Name = "PyPy",
                Description = "Bites like a snake, feels like honey.",
                Flavor = "Slow",
                Price = 50,
            }
        ];

        _context.SaveChanges();
    }

    private string CreateGuid() => Guid.NewGuid().ToString();
}
