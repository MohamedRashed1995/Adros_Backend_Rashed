using Adros.Core.Entities.Course;
using Adros.Core.Entities.Users;
using Adros.Core.Enums;
using Adros.Shared;

namespace Adros.Core.Entities.Home
{
    public class Stage : BaseEntity
    {
        public string? ImageName { get; set; }
        public string Title { get; set; } = default!;
        public int? Order { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public StageType? Type { get; set; }
        public ICollection<Level> Levels { get; set; } = [];
    }
}
