using Adros.Core.Entities.Course;
using Adros.Shared;

namespace Adros.Core.Entities.Users
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Government { get; set; }
        public string? City { get; set; }
        public DateOnly? BirthDate { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int LoginTimes { get; set; }
        public ICollection<VideoView> VideoViews { get; set; } = [];
        public ICollection<VideoDownload> VideoDownloads { get; set; } = [];
        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }
        public string SubscriptionStatus { get; set; }
    }
}
