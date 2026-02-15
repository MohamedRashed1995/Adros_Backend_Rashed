using Adros.Core.Entities.Assessements;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class Unit : BaseEntity
    {
        public string Title { get; set; } = default!;

        #region Relations
        //public Guid LessonId { get; set; }
        public ICollection<Lesson>  Lessons { get; set; } = new List<Lesson>();

        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public string Description { get; set; } = string.Empty;

        // prerequisite topics
        //public ICollection<Topic> Prerequisites { get; set; } = [];

        // postrequisite topics
        //public ICollection<Topic> Postrequisites { get; set; } = [];

        // questions
        public ICollection<Question> Questions { get; set; } = new List<Question>();

        // assessments
        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

        // topic Videos
        //public ICollection<Video> Videos { get; set; } = [];
        #endregion

    }
}
