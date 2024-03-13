using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class CompanyInfoConfiguration : IEntityTypeConfiguration<CompanyInfoPOCO>
    {
        public void Configure(EntityTypeBuilder<CompanyInfoPOCO> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(150);
            builder.Property(r => r.Description).HasMaxLength(500);
            builder.Property(r => r.NIP).IsRequired().HasMaxLength(20);

            builder.HasMany(p => p.Users)
                .WithOne(r => r.Company);
        }
    }
}