using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;
internal class BeerConfiguration : IEntityTypeConfiguration<Beer>
{
    public void Configure(EntityTypeBuilder<Beer> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name).HasMaxLength(DomainConfig.NameLength);
        builder.Property(f => f.Flavor).HasMaxLength(DomainConfig.NameLength);

        builder
            .HasOne(f => f.Brewer)
            .WithMany(f => f.Beers);
    }
}
