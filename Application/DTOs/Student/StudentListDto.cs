namespace Adros.Application.DTOs.Student
{
    public class StudentListDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? lastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
<<<<<<< HEAD
        public bool IsSubscriped { get; set; }
=======
        public bool IsActive { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public DateTime CreatedAt { get; set; }
        public string? Government { get; set; }
        public string? City { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Level { get; set; }
        public string? Stage { get; set; }
        public int LoginTimes { get; set; }
        public int ViewsCount { get; set; }
<<<<<<< HEAD
        public int WatchLaterCount { get; set; }
=======
        public int DownloadsCount { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
