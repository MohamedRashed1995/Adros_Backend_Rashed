using Adros.Core.Entities.Course;
using Adros.Core.Enums;
using Adros.Shared;

namespace Adros.Core.Entities.Home
{
    public class Stage : BaseEntity
    {
        public string ImageName { get; set; } = default!;
        public string Title { get; set; } = default!;
        public int? Order { get; set; }

        public StageType Type { get; set; }
        public ICollection<Level> Levels { get; set; } = [];
    }
}
