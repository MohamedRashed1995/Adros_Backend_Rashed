using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
    public class UnitConfigurations : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            // Configure primary key
            builder.HasKey(x => x.Id);

            // Configure properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                  .HasDefaultValueSql("GETUTCDATE()"); ;

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                 .HasDefaultValueSql("GETUTCDATE()"); ;

            builder.Property(x => x.CreatedBy)
                .IsRequired();

            builder.Property(x => x.UpdatedBy)
                .IsRequired();

            builder.Property(x => x.Deleted)
                .IsRequired();

            // Configure relationships
            builder.HasMany(t => t.Lessons)
               .WithOne(l => l.Unit)
               .HasForeignKey(l => l.UnitId)
               .OnDelete(DeleteBehavior.Cascade); // أو NoAction حسب ما تحب


            //builder.HasMany(x => x.Prerequisites)
            //    .WithMany(x => x.Postrequisites)
            //    .UsingEntity(j => j.ToTable("TopicPrerequisites"));

            builder.HasMany(x => x.Questions)
                .WithOne(x => x.Topic)
                .HasForeignKey(Q => Q.TopicId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
