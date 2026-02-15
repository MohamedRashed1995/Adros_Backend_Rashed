using Adros.Core.Entities.Users;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class VideoDownload : BaseEntity
    {
        public Guid VideoId { get; set; }
        public Video Video { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
