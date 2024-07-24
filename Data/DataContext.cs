using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Beer> Beers { get; set; }
    public DbSet<BeerSale> BeerSales { get; set; }
    public DbSet<Brewer> Brewers { get; set; }
    public DbSet<Brewery> Breweries { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Wholesaler> Wholesalers { get; set; }
    public DbSet<WholesalerInventory> WholesalerInventories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}
