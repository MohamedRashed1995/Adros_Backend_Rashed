using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
    public class LessonVideoDownloadConfigurations : IEntityTypeConfiguration<VideoDownload>
    {
        public void Configure(EntityTypeBuilder<VideoDownload> builder)
        {
            builder
                .HasOne(lvd => lvd.Student)
                .WithMany(S => S.VideoDownloads)
                .HasForeignKey(lvd => lvd.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(lvd => lvd.Video)
                .WithMany(V => V.Downloads)
                .HasForeignKey(lvd => lvd.VideoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
