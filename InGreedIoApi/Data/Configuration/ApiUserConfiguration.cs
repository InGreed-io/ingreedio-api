using InGreedIoApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class ApiUserConfiguration : IEntityTypeConfiguration<ApiUser>
    {
        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.IsBlocked).HasDefaultValue(false);

            builder.HasOne(p => p.Company)
                .WithMany(r => r.Users)
                .HasForeignKey(p => p.CompanyId)
                .IsRequired(false);

            builder.HasMany(p => p.FavouriteProducts)
                .WithMany(r => r.FavouriteBy);

            builder.HasMany(p => p.ProduceProducts)
               .WithOne(r => r.Producer);
        }
    }
}