
namespace Application.Services;

public interface ISaleService
{
    WholesalerInventory? CreateSale(IList<WholesalerInventory> wholesalerInventories, Wholesaler wholesaler, Beer beer, int quantity);
}