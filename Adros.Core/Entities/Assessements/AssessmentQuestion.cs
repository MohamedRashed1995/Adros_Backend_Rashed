namespace Adros.Core.Entities.Assessements
{
    public class AssessmentQuestion
    {
        public Guid AssessmentId { get; set; }
        public virtual Assessment Assessment { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
