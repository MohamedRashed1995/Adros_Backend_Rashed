namespace Adros.Application.DTOs.Teacher
{
    public class TeacherEntityDto
    {
        public Guid TeacherID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public Guid? StageId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }   
        public string? PhoneNumber { get; set; }
        public int LessonCount { get; set; }
    }
}
