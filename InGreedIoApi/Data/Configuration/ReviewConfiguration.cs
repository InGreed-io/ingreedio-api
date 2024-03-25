using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.POCO;

namespace InGreedIoApi.Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<ReviewPOCO>
    {
        public void Configure(EntityTypeBuilder<ReviewPOCO> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Text).IsRequired().HasMaxLength(500);
            builder.Property(r => r.Rating).IsRequired();

            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.ProductId);

            builder.HasOne(r => r.User)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.UserID);
        }
    }
}