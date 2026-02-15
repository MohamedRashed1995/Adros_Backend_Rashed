using Adros.Core.Entities.Home;
using Adros.Core.Entities.Users;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class Level : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? ImageName { get; set; }
        public Guid StageId { get; set; }
        public Stage Stage { get; set; }
        public string? StageName { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
