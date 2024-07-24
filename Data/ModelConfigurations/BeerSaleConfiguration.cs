using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;
internal class BeerSaleConfiguration : IEntityTypeConfiguration<BeerSale>
{
    public void Configure(EntityTypeBuilder<BeerSale> builder)
    {
        builder.HasKey(f => f.Id);

        builder.HasOne(f => f.Beer);
        builder.HasOne(f => f.Wholesaler);
    }
}
