using Adros.Core.Enums;
using Adros.Shared;
namespace Adros.Core.Entities.Course
{
    public class Video : BaseEntity
    {
        public string Title { get; set; } = default!;
        public TimeOnly Duration { get; set; }
        public string Url { get; set; } = default!;
        public int? Order { get; set; }
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        public Guid TopicId { get; set; }
        public virtual Topic Topic { get; set; }

        public ICollection<VideoView> Views { get; set; } = [];
        public ICollection<VideoDownload> Downloads { get; set; } = [];

        public string BunnyVideoId { get; set; } 
        public VideoStatus Status { get; set; } = VideoStatus.Processing;
        public DateTime? ProcessedAt { get; set; }
    }
}
