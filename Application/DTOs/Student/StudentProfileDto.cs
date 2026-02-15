namespace Adros.Application.DTOs.Student
{
    public class StudentProfileDto
    {
        public string StudentId { get; set; }
        public string? ImagePath { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public Guid? LevelId { get; set; }
        public int ViewsCount { get; set; }
        public int Watchlatercount { get; set; }
        public int LessonCount { get; set; }
        public bool IsSubscribed { get; set; }
        public TimeSpan TotalStudyTime { get; set; }
        public List<StudentDailyAchievement> DailyAchievements { get; set; } = new();
    }

    
}