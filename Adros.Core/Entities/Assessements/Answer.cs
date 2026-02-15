using Adros.Shared;
namespace Adros.Core.Entities.Assessements
{
    public class Answer : BaseEntity
    {
        public string Text { get; set; } = default!;
        public int? Order { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
