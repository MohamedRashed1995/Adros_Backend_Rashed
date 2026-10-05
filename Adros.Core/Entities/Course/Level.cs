using Adros.Core.Entities.Home;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class Level : BaseEntity
    {
        public string Title { get; set; } = default!;
        public Guid StageId { get; set; }
        public Stage Stage { get; set; }
        public ICollection<Subject> Subjects { get; set; } = [];
    }
}
