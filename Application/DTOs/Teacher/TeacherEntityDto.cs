namespace Adros.Application.DTOs.Teacher
{
    public class TeacherEntityDto
    {
        public Guid TeacherID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
<<<<<<< HEAD
        public string? ProfilePictureUrl { get; set; }
        public Guid? StageId { get; set; }
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public Guid ApplicationUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }   
<<<<<<< HEAD
        public string? PhoneNumber { get; set; }
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public int LessonCount { get; set; }
    }
}
