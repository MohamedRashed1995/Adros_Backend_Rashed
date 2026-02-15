namespace Adros.Application.DTOs.Level
{
    public class LevelEntityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid StageId { get; set; }
        public int StudentsCount { get; set; }
        public Guid CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? StageName { get; set; }
        public string ImagePath { get; set; } = default!;
    }
}
