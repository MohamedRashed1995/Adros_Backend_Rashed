namespace Adros.Application.DTOs.Teacher
{
    public class TeacherEntityDto
    {
        public Guid TeacherId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int LessonCount { get; set; }
    }
}
