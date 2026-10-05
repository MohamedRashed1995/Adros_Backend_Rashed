using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
    public class LessonConfigurations : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            
        }
    }
}
