using Adros.Core.Entities.Subscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Infrastructure.Data.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
<<<<<<< HEAD
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
=======
            builder.HasKey(s => s.Id);

            builder.Property(s => s.PaymentTransactionId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            builder.Property(s => s.AmountPaid)
                .HasColumnType("decimal(18,2)");

            // Relationships
            builder.HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.SubscriptionPlan)
                .WithMany(sp => sp.Subscriptions)
                .HasForeignKey(s => s.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(s => s.StudentId);
            builder.HasIndex(s => s.SubscriptionPlanId);
            builder.HasIndex(s => s.Status);
            builder.HasIndex(s => s.EndDate);
        }
    }

    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sp => sp.Description)
                .HasMaxLength(500);

            builder.Property(sp => sp.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(sp => sp.PlanType)
                .IsRequired()
                .HasMaxLength(50);

            // Seed data
            builder.HasData(
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Name = "Basic Monthly",
                    Description = "Basic monthly subscription",
                    Price = 9.99m,
                    DurationInDays = 30,
                    PlanType = "monthly",
                    IsActive = true,
                    //CreatedAt = DateTime.UtcNow
                },
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Name = "Premium Monthly",
                    Description = "Premium monthly subscription",
                    Price = 19.99m,
                    DurationInDays = 30,
                    PlanType = "monthly",
                    IsActive = true,
                    //CreatedAt = DateTime.UtcNow
                },
                new SubscriptionPlan
                {
                    Id = Guid.NewGuid(),
                    Name = "Annual Plan",
                    Description = "Annual subscription with discount",
                    Price = 99.99m,
                    DurationInDays = 365,
                    PlanType = "annual",
                    IsActive = true,
                    //CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
