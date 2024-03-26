using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationTypePOCO>
    {
        public void Configure(EntityTypeBuilder<OperationTypePOCO> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);

            builder.HasMany(p => p.Operations)
                .WithOne(p => p.OperationType);
        }
    }
}