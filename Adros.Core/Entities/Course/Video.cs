using Adros.Core.Enums;
using Adros.Shared;
namespace Adros.Core.Entities.Course
{

    public class Video : BaseEntity
    {

        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        //public string? ThumbnailUrl { get; set; }
        public int? Duration { get; set; }
        public string Url { get; set; } = default!;
        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = default!;

        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; } = default!;
        public VideoSourceType SourceType { get; set; }
        public ICollection<VideoView> Views { get; set; } = new List<VideoView>();
        public ICollection<WatchLater> Watchlater { get; set; } = new List<WatchLater>();

        public string? BunnyVideoId { get; set; }
        public VideoStatus Status { get; set; } = VideoStatus.Processing;
        public DateTime? ProcessedAt { get; set; }
    }
}
public enum VideoSourceType
{
    Upload = 1,
    ExternalLink = 2
}
