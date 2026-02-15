using Adros.Core.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adros.Persistence.Configurations.Course
{
    public class LessonVideoDownloadConfigurations : IEntityTypeConfiguration<WatchLater>
    {
        public void Configure(EntityTypeBuilder<WatchLater> builder)
        {
            builder
                .HasOne(lvd => lvd.Student)
                .WithMany(S => S.WatchLater)
                .HasForeignKey(lvd => lvd.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(lvd => lvd.Video)
                .WithMany(V => V.Watchlater)
                .HasForeignKey(lvd => lvd.VideoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
