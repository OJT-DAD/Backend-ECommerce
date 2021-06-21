using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.Stock)
                .WithOne(x => x.Product)
                .HasForeignKey<Stock>(x => x.ProductId);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(14,2)");

        }
    }
}
