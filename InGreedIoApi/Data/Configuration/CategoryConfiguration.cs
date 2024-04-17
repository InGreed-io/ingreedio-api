using InGreedIoApi.Data.Seed;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryPOCO>
    {
        public void Configure(EntityTypeBuilder<CategoryPOCO> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.Products)
            .WithOne(r => r.Category);
        }
    }
}