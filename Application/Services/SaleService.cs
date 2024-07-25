namespace Application.Services;
public class SaleService : ISaleService
{
    public WholesalerInventory? CreateSale(
        IList<WholesalerInventory> wholesalerInventories,
        Wholesaler wholesaler,
        Beer beer,
        int quantity)
    {
        var item = wholesalerInventories.FirstOrDefault(f => f.BeerId == beer.Id);

        if (item is null)
        {
            item = new()
            {
                BeerId = beer.Id,
                WholesalerId = wholesaler.Id,
                Quantity = quantity,
            };

            return item;
        }
        else
        {
            item.Quantity += quantity;
            return null;
        }
    }
}
