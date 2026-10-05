namespace Adros.Application.DTOs.Student
{
    public class StudentDailyAchievement
    {
        public string Day { get; set; } = string.Empty; // Format: "yyyy-MM-dd"
        public TimeSpan StudyTime { get; set; }
    }
}
