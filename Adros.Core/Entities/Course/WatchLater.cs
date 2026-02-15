using Adros.Core.Entities.Users;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class WatchLater : BaseEntity
    {
        public Guid VideoId { get; set; }
        public virtual Video Video { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
