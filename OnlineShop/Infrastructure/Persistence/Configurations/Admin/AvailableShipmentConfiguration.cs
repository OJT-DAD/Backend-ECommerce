using Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations.Admin
{
    public class AvailableShipmentConfiguration : IEntityTypeConfiguration<AvailableShipment>
    {
        public void Configure(EntityTypeBuilder<AvailableShipment> builder)
        {
            builder.Property(x => x.ShipmentCost)
                .HasColumnType("decimal(14,2)");
        }
    }
}
