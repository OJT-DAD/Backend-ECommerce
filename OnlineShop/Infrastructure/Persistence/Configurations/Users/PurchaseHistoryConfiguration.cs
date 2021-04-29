using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations.Users
{
    public class PurchaseHistoryConfiguration : IEntityTypeConfiguration<PurchaseHistory>
    {
        public void Configure(EntityTypeBuilder<PurchaseHistory> builder)
        {
            builder.Property(x => x.TotalPrice)
                .HasColumnType("decimal(14,2)");

            builder.Property(x => x.UnitPrice)
                .HasColumnType("decimal(14,2)");
        }
    }
}
