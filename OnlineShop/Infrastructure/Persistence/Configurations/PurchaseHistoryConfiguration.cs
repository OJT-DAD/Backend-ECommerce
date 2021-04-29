using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PurchaseHistoryConfiguration : IEntityTypeConfiguration<PurchaseHistory>
    {
        public void Configure(EntityTypeBuilder<PurchaseHistory> builder)
        {
            builder.Property(x => x.TotalPrice)
                .HasColumnType("decimal(14, 2)");
        }
    }
}
