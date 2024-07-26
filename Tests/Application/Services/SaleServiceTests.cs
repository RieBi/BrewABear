using Application.Queries.BrewerQueries;
using Application.Services;
using Domain.Models;

namespace Tests.Application.Services;
public class SaleServiceTests
{
    static List<Beer> GetBeers =>
        [
            new() { Id = "1", Name = "Test1" },
            new() { Id = "2", Name = "Test2" },
        ];

    static List<Wholesaler> GetWholesalers =>
        [
            new() { Id = "W1", Name = "Saler1" },
            new() { Id = "W2", Name = "Saler2" },
        ];

    public static TheoryData<List<WholesalerInventory>, Beer, Wholesaler, int, List<WholesalerInventory>> GetExistingInventories()
    {
        var beers = GetBeers;

        List<Wholesaler> salers = GetWholesalers;

        List<WholesalerInventory> inventories =
        [
            new()
            {
                Beer = beers[0], BeerId = beers[0].Id,
                Wholesaler = salers[0], WholesalerId = salers[0].Id,
                Quantity = 5,
            },
            new()
            {
                Beer = beers[1], BeerId = beers[1].Id,
                Wholesaler = salers[1], WholesalerId = salers[1].Id,
                Quantity = 1,
            },
        ];

        List<int> quantities =
        [
            1,
            10,
        ];

        List<WholesalerInventory> expectedInventories =
        [
            new()
            {
                Beer = beers[0], BeerId = beers[0].Id,
                Wholesaler = salers[0], WholesalerId = salers[0].Id,
                Quantity = 6,
            },
            new()
            {
                Beer = beers[1], BeerId = beers[1].Id,
                Wholesaler = salers[1], WholesalerId = salers[1].Id,
                Quantity = 11
            },
        ];

        return new()
        {
            { [inventories[0]], beers[0], salers[0], quantities[0], [expectedInventories[0]] },
            { [inventories[1]], beers[1], salers[1], quantities[1], [expectedInventories[1]] },
        };
    }

    public static TheoryData<List<WholesalerInventory>, Beer, Wholesaler, int, List<WholesalerInventory>> GetUpdatedInventories()
    {
        var beers = GetBeers;
        var salers = GetWholesalers;

        List<WholesalerInventory> inventory =
            [   
                new()
                { Beer = beers[1], BeerId = beers[1].Id,
                    Wholesaler = salers[1], WholesalerId = salers[1].Id,
                    Quantity = 10
                }
            ];

        List<WholesalerInventory> expectedInventory =
            [
                new()
                { Beer = beers[1], BeerId = beers[1].Id,
                    Wholesaler = salers[1], WholesalerId = salers[1].Id,
                    Quantity = 10
                }
            ];

        return new()
        {
            { [], beers[0], salers[0], 5, [] },
            { inventory, beers[0], salers[0], 10, expectedInventory }
        };
    }

    [Theory]
    [MemberData(nameof(GetExistingInventories))]
    public void UpdatesExistingInventory(
        List<WholesalerInventory> inventory,
        Beer beer,
        Wholesaler wholesaler,
        int quantity,
        List<WholesalerInventory> expected)
    {
        var service = new SaleService();

        var result = service.CreateSale(inventory, wholesaler, beer, quantity);

        Assert.Null(result);
        Assert.Equivalent(expected, inventory);
    }

    [Theory]
    [MemberData(nameof(GetUpdatedInventories))]
    public void CreatesNewInventory(
        List<WholesalerInventory> inventory,
        Beer beer,
        Wholesaler wholesaler,
        int quantity,
        List<WholesalerInventory> expected)
    {
        var service = new SaleService();

        var result = service.CreateSale(inventory, wholesaler, beer, quantity);

        Assert.Equivalent(expected, inventory);
        Assert.NotNull(result);
    }
}
