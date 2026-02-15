using Adros.Core.Entities.Course;
using Adros.Shared;

namespace Adros.Core.Entities.Users
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
<<<<<<< HEAD
        public string? PhoneNumber { get; set; }
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public string? Government { get; set; }
        public string? City { get; set; }
        public DateOnly? BirthDate { get; set; }
        public Guid ApplicationUserId { get; set; }
<<<<<<< HEAD
        public ApplicationUser? ApplicationUser { get; set; }
        public int LoginTimes { get; set; }
        public ICollection<VideoView> VideoViews { get; set; } = new List<VideoView>();
        public ICollection<WatchLater> WatchLater { get; set; } = new List<WatchLater>();
        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }
        public bool IsSubscriped { get; set; }
=======
        public ApplicationUser ApplicationUser { get; set; }
        public int LoginTimes { get; set; }
        public ICollection<VideoView> VideoViews { get; set; } = new List<VideoView>();
        public ICollection<VideoDownload> VideoDownloads { get; set; } = new List<VideoDownload>();
        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }
        public string SubscriptionStatus { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
