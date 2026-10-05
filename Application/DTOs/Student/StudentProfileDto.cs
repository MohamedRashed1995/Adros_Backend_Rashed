using Adros.Core.Entities;

namespace Adros.Application.DTOs.Student
{
    public class StudentProfileDto
    {
        public string? ImagePath { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        //public ApplicationUser User { get; set; }
        //public int ViewsCount { get; set; }
        public int DownloadsCount { get; set; }
        public TimeSpan TotalStudyTime { get; set; }

        public List<StudentDailyAchievement> DailyAchievements { get; set; } = [];
    }

}
