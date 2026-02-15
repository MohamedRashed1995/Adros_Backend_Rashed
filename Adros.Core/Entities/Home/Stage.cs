using Adros.Core.Entities.Course;
<<<<<<< HEAD
using Adros.Core.Entities.Users;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Core.Enums;
using Adros.Shared;

namespace Adros.Core.Entities.Home
{
    public class Stage : BaseEntity
    {
        public string? ImageName { get; set; }
        public string Title { get; set; } = default!;
        public int? Order { get; set; }
<<<<<<< HEAD
        public ICollection<Teacher> Teachers { get; set; }
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public StageType? Type { get; set; }
        public ICollection<Level> Levels { get; set; } = [];
    }
}
