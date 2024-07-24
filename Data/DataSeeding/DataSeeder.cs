namespace Data.DataSeeding;
public class DataSeeder(DataContext context)
{
    private readonly DataContext _context = context;

    public void ApplySeeding()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Breweries.AddRange(
            [
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bear Brewery",
                    Address = "Prague, Big Street, 752/13",
                },
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Alien Hot Brewery",
                    Address = "Mars, Small Crater, Loophole 17, 1",
                }
            ]
        );

        _context.SaveChanges();
    }
}
