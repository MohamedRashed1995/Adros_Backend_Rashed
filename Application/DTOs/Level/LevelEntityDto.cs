namespace Adros.Application.DTOs.Level
{
    public class LevelEntityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
