using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
    public class TopicConfigurations : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            // Configure primary key
            builder.HasKey(x => x.Id);

            // Configure properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .IsRequired();

            builder.Property(x => x.UpdatedBy)
                .IsRequired();

            builder.Property(x => x.Deleted)
                .IsRequired();

            // Configure relationships
            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.Topics)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Prerequisites)
                .WithMany(x => x.Postrequisites)
                .UsingEntity(j => j.ToTable("TopicPrerequisites"));

            builder.HasMany(x => x.Questions)
                .WithOne(x => x.Topic)
                .HasForeignKey(Q => Q.TopicId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
