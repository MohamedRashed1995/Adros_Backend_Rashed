using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Persistence.Configurations.Course
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
