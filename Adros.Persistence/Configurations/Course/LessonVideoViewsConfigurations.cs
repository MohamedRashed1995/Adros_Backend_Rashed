using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
    public class LessonVideoViewsConfigurations : IEntityTypeConfiguration<VideoView>
    {
        public void Configure(EntityTypeBuilder<VideoView> builder)
        {
            builder
               .HasOne(lvv => lvv.Student)
               .WithMany(S => S.VideoViews)
               .HasForeignKey(lvv => lvv.StudentId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(lvv => lvv.Video)
                .WithMany(V => V.Views)
                .HasForeignKey(lvv => lvv.VideoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
