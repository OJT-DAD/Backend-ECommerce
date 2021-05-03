using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserPropertyConfiguration : IEntityTypeConfiguration<UserProperty>
    {
        public void Configure(EntityTypeBuilder<UserProperty> builder)
        {
            builder.Property(x => x.Username)
                .IsRequired();
        }
    }
}
