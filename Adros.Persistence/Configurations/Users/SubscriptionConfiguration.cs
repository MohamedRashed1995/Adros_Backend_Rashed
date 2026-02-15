using Adros.Core.Entities.Subscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Infrastructure.Data.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            // المفتاح الأساسي
            builder.HasKey(s => s.Id);

            // الخصائص
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Price)
                .IsRequired()
                .HasMaxLength(50); // لأنه string دلوقتي

            builder.Property(s => s.Duration)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Benefits)
                .HasMaxLength(1000);

            // خصائص BaseEntity
            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.UpdatedAt);

            builder.Property(s => s.Deleted)
                .HasDefaultValue(false);

            builder.Property(s => s.CreatedBy);
            builder.Property(s => s.UpdatedBy);

            // Indexes
            builder.HasIndex(s => s.Name);
            builder.HasIndex(s => s.Deleted);
        }
    }
}
