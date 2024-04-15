using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class FeaturingConfiguration : IEntityTypeConfiguration<FeaturingPOCO>
    {
        public void Configure(EntityTypeBuilder<FeaturingPOCO> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(p => p.Product)
                .WithOne(r => r.Featuring)
                .HasForeignKey<FeaturingPOCO>(p => p.ProductId);
        }
    }
}