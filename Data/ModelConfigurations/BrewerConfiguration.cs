using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;
internal class BrewerConfiguration : IEntityTypeConfiguration<Brewer>
{
    public void Configure(EntityTypeBuilder<Brewer> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.FirstName).HasMaxLength(DomainConfig.NameLength);
        builder.Property(f => f.LastName).HasMaxLength(DomainConfig.NameLength);
        builder.Property(f => f.ContactEmail).HasMaxLength(DomainConfig.NameLength);

        builder
            .HasMany(f => f.Beers)
            .WithOne(f => f.Brewer);

        builder
            .HasOne(f => f.Brewery)
            .WithMany(f => f.Brewers);
    }
}
