namespace Adros.Application.DTOs.Stage
{
    public class StageEntityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public int? Order { get; set; }
    }
}
