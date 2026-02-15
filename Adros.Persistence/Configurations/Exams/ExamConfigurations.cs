using Adros.Core.Entities.Assessements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Exams
{
    public class ExamConfigurations : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasOne(A => A.Unit)
                   .WithMany( T => T.Assessments)
                   .HasForeignKey(A => A.UnitId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
