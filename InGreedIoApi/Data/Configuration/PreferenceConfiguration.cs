using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class PreferenceConfiguration : IEntityTypeConfiguration<PreferencePOCO>
    {
        public void Configure(EntityTypeBuilder<PreferencePOCO> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.IsActive).IsRequired();

            builder.HasMany(p => p.Wanted);

            builder.HasMany(p => p.Unwanted);
        }
    }
}