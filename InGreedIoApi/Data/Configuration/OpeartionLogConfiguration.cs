using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class OperationLogConfiguration : IEntityTypeConfiguration<OperationLogPOCO>
    {
        public void Configure(EntityTypeBuilder<OperationLogPOCO> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TimeStamp).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Details).IsRequired().HasMaxLength(500);
            builder.Property(p => p.OperationTypeId).IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(r => r.Operations)
                .HasForeignKey(r => r.UserId);

            builder.HasOne(p => p.OperationType)
                .WithMany(r => r.Operations)
                .HasForeignKey(r => r.OperationTypeId);
        }
    }
}