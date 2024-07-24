﻿namespace Domain.Models;
public class Wholesaler
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;

    public IList<WholesalerInventory> InventoryItems { get; set; } = [];
}
