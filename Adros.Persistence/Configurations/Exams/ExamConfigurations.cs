using Adros.Core.Entities.Assessements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Exams
{
    public class ExamConfigurations : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
<<<<<<< HEAD
            builder.HasOne(A => A.Unit)
                   .WithMany( T => T.Assessments)
                   .HasForeignKey(A => A.UnitId)
=======
            builder.HasOne(A => A.Topic)
                   .WithMany( T => T.Assessments)
                   .HasForeignKey(A => A.TopicId)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
