using Adros.Core.Entities.Users;

namespace Adros.Core.Entities.Course
{
    public class StudentProgress
    {
        #region Relations
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public Guid TopicId { get; set; }
        public virtual Unit Topic { get; set; }
        #endregion

        #region Props
        public string ProficiencyLevel { get; set; } = default!;
        #endregion
    }
}
