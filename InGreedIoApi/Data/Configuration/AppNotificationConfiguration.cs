using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InGreedIoApi.Data.Configuration
{
    public class AppNotificationConfiguration : IEntityTypeConfiguration<AppNotificationPOCO>
    {
        public void Configure(EntityTypeBuilder<AppNotificationPOCO> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Seen).IsRequired(false);

            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Content).IsRequired().HasMaxLength(500);
            builder.Property(c => c.NotificationType).IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(r => r.AppNotifications)
                .HasForeignKey(p => p.UserId);
        }
    }
}