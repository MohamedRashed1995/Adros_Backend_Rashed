namespace Adros.Application.DTOs.Level
{
    public class LevelEntityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid StageId { get; set; }
<<<<<<< HEAD
        public int StudentsCount { get; set; }
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public Guid CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? StageName { get; set; }
        public string ImagePath { get; set; } = default!;
    }
}
