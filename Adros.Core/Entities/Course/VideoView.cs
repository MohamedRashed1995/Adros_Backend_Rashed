using Adros.Core.Entities.Users;
using Adros.Shared;

namespace Adros.Core.Entities.Course
{
    public class VideoView : BaseEntity
    {
        public Guid VideoId { get; set; }
        public Video Video { get; set; }
        public TimeSpan Duration { get; set; }
<<<<<<< HEAD
        public int LastReportedSecond { get; set; }
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
