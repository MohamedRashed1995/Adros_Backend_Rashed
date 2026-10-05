using Adros.Core.Entities.Assessements;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class Topic : BaseEntity
    {
        public string Title { get; set; } = default!;

        #region Relations
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        // prerequisite topics
        public ICollection<Topic> Prerequisites { get; set; } = [];

        // postrequisite topics
        public ICollection<Topic> Postrequisites { get; set; } = [];

        // questions
        public ICollection<Question> Questions { get; set; } = [];

        // assessments
        public ICollection<Assessment> Assessments { get; set; } = [];

        // topic Videos
        public ICollection<Video> Videos { get; set; } = [];
        #endregion

    }
}
