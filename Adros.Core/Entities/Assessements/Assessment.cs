using Adros.Core.Entities.Course;
using Adros.Shared;

namespace Adros.Core.Entities.Assessements
{
    public class Assessment : BaseEntity
    {
        public Guid TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public string Title { get; set; } = default!;
        public int Score { get; set; }
        public ICollection<AssessmentQuestion> Questions { get; set; } = [];
    }
}
