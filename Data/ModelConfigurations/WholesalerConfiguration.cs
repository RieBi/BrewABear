using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;
internal class WholesalerConfiguration : IEntityTypeConfiguration<Wholesaler>
{
    public void Configure(EntityTypeBuilder<Wholesaler> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name).HasMaxLength(DomainConfig.NameLength);

        builder
            .HasMany(f => f.InventoryItems)
            .WithOne(f => f.Wholesaler);
    }
}
