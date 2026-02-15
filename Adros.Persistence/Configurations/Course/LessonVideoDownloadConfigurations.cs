using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
<<<<<<< HEAD
    public class LessonVideoDownloadConfigurations : IEntityTypeConfiguration<WatchLater>
    {
        public void Configure(EntityTypeBuilder<WatchLater> builder)
        {
            builder
                .HasOne(lvd => lvd.Student)
                .WithMany(S => S.WatchLater)
=======
    public class LessonVideoDownloadConfigurations : IEntityTypeConfiguration<VideoDownload>
    {
        public void Configure(EntityTypeBuilder<VideoDownload> builder)
        {
            builder
                .HasOne(lvd => lvd.Student)
                .WithMany(S => S.VideoDownloads)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                .HasForeignKey(lvd => lvd.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(lvd => lvd.Video)
<<<<<<< HEAD
                .WithMany(V => V.Watchlater)
=======
                .WithMany(V => V.Downloads)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                .HasForeignKey(lvd => lvd.VideoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
