using Adros.Core.Entities.Course;
using Adros.Core.Enums;
using Adros.Shared;

namespace Adros.Core.Entities.Assessements
{
    public class Question : BaseEntity
    {
        public string Title { get; set; } = default!;
        public QuestionType QuestionType { get; set; }

        #region Relations
        public Guid TopicId { get; set; }
        public virtual Unit Topic { get; set; }

        public Guid DificultyLevelId { get; set; }
        public virtual DificultyLevel DificultyLevel { get; set; }

        public ICollection<Answer> Answers { get; set; } = [];
        public ICollection<AssessmentQuestion> Assessments { get; set; } = [];
        #endregion

    }
}
