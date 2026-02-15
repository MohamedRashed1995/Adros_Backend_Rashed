using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class Attachment : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Url { get; set; } = default!;
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
