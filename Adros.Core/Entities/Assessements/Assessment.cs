using Adros.Core.Entities.Course;
using Adros.Shared;

namespace Adros.Core.Entities.Assessements
{
    public class Assessment : BaseEntity
    {
<<<<<<< HEAD
        public Guid UnitId { get; set; }
        public virtual Unit Unit{ get; set; }
=======
        public Guid TopicId { get; set; }
        public virtual Unit Topic { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public string Title { get; set; } = default!;
        public int Score { get; set; }
        public ICollection<AssessmentQuestion> Questions { get; set; } = [];
    }
}
