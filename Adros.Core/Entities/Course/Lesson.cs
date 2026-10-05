using Adros.Core.Entities.Assessements;
using Adros.Core.Entities.Users;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class Lesson : BaseEntity
    {
        public int Order { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public ICollection<Attachment> Attachments { get; set; } = [];
        public ICollection<Topic> Topics { get; set; } = [];
        public ICollection<Video> Videos { get; set; } = [];

        public Guid? ExamId { get; set; }
        //public virtual Assessment? Exam { get; set; }
    }
    public class LessonDto
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; } = default!;
        public Guid? ExamId { get; set; }
        public string? ExamTitle { get; set; }
        public List<string> Attachments { get; set; } = new();
        public List<string> Videos { get; set; } = new();
    }
}
