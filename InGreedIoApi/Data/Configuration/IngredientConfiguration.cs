using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<IngredientPOCO>
    {
        public void Configure(EntityTypeBuilder<IngredientPOCO> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.Products)
                .WithMany(r => r.Ingredients);
        }
    }
}