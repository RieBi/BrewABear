using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;
internal class BreweryConfiguration : IEntityTypeConfiguration<Brewery>
{
    public void Configure(EntityTypeBuilder<Brewery> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name).HasMaxLength(DomainConfig.NameLength);
        builder.Property(f => f.Address).HasMaxLength(DomainConfig.AddressLength);

        builder
            .HasMany(f => f.Brewers)
            .WithOne(f => f.Brewery);
    }
}
