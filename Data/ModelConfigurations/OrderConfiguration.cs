using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.ClientEmail).HasMaxLength(DomainConfig.NameLength);

        builder.HasOne(f => f.Beer);
        builder.HasOne(f => f.Wholesaler);
    }
}
