using Adros.Shared;

namespace Adros.Core.Entities.Assessements
{
    public class DificultyLevel : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<Question> Questions { get; set; } = [];
    }
}
