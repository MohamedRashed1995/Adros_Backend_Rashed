using Adros.Core.Enums;
using Adros.Shared;
namespace Adros.Core.Entities.Course
{
<<<<<<< HEAD

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
=======
    public class Video : BaseEntity
    {
        public string Title { get; set; } = default!;
        public TimeSpan Duration { get; set; }
        public string Url { get; set; } = default!;
        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        public Guid UnitId { get; set; }
        public virtual Unit Topic { get; set; }

        public ICollection<VideoView> Views { get; set; } = [];
        public ICollection<VideoDownload> Downloads { get; set; } = [];
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

        public string? BunnyVideoId { get; set; }
        public VideoStatus Status { get; set; } = VideoStatus.Processing;
        public DateTime? ProcessedAt { get; set; }
    }
}
<<<<<<< HEAD
public enum VideoSourceType
{
    Upload = 1,
    ExternalLink = 2
}
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
