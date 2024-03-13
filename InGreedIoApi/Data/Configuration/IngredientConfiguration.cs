using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.POCO;

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