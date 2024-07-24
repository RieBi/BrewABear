using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;
internal class WholesalerInventoryConfiguration : IEntityTypeConfiguration<WholesalerInventory>
{
    public void Configure(EntityTypeBuilder<WholesalerInventory> builder)
    {
        builder.HasKey(f => new object[] { f.Beer, f.Wholesaler });

        builder
            .HasOne(f => f.Wholesaler)
            .WithMany(f => f.InventoryItems);

        builder.HasOne(f => f.Beer);
    }
}
