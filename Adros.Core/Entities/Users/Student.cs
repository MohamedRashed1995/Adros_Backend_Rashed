using Adros.Core.Entities.Course;
using Adros.Shared;

namespace Adros.Core.Entities.Users
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Government { get; set; }
        public string? City { get; set; }
        public DateOnly? BirthDate { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public int LoginTimes { get; set; }
        public ICollection<VideoView> VideoViews { get; set; } = new List<VideoView>();
        public ICollection<WatchLater> WatchLater { get; set; } = new List<WatchLater>();
        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }
        public bool IsSubscriped { get; set; }
    }
}
