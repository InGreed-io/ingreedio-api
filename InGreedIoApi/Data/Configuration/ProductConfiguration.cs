using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductPOCO>
    {
        public void Configure(EntityTypeBuilder<ProductPOCO> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);

            builder.HasOne(p => p.Featuring)
                   .WithOne(r => r.Product)
                   .HasForeignKey<ProductPOCO>(p => p.FeaturingId)
                   .IsRequired(false);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId);

            builder.HasMany(p => p.Ingredients)
                   .WithMany(i => i.Products);

            builder.HasMany(p => p.Reviews)
                   .WithOne(r => r.Product)
                   .HasForeignKey(r => r.ProductId);

            builder.HasMany(p => p.FavouriteBy)
                .WithMany(r => r.FavouriteProducts);

            builder.HasOne(p => p.Producer)
                .WithMany(r => r.ProduceProducts)
                .HasForeignKey(p => p.ProducerId);
        }
    }
}