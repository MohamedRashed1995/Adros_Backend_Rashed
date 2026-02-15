using Adros.Core.Entities.Assessements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Exams
{
    public class AssessmentQuestionConfigurations : IEntityTypeConfiguration<AssessmentQuestion>
    {
        public void Configure(EntityTypeBuilder<AssessmentQuestion> builder)
        {
            builder.HasKey(x => new { x.AssessmentId, x.QuestionId });
        }
    }
}
