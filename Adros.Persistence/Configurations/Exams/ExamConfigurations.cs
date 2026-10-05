using Adros.Core.Entities.Assessements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Exams
{
    public class ExamConfigurations : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasOne(A => A.Topic)
                   .WithMany( T => T.Assessments)
                   .HasForeignKey(A => A.TopicId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
