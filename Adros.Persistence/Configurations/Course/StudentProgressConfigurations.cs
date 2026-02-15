using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
    public class StudentProgressConfigurations : IEntityTypeConfiguration<StudentProgress>
    {
        public void Configure(EntityTypeBuilder<StudentProgress> builder)
        {
            builder.HasKey(sp => new { sp.StudentId, sp.TopicId });

            builder.Property(sp => sp.ProficiencyLevel)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(sp => sp.Student)
                   .WithMany()
                   .HasForeignKey(sp => sp.StudentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(sp => sp.Topic)
                   .WithMany()
                   .HasForeignKey(sp => sp.TopicId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
