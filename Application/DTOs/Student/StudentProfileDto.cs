namespace Adros.Application.DTOs.Student
{
    public class StudentProfileDto
    {
<<<<<<< HEAD
        public string StudentId { get; set; }
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public string? ImagePath { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public Guid? LevelId { get; set; }
        public int ViewsCount { get; set; }
<<<<<<< HEAD
        public int Watchlatercount { get; set; }
        public int LessonCount { get; set; }
        public bool IsSubscribed { get; set; }
=======
        public int DownloadsCount { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public TimeSpan TotalStudyTime { get; set; }
        public List<StudentDailyAchievement> DailyAchievements { get; set; } = new();
    }

    
}