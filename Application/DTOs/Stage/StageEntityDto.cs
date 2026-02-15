using Adros.Core.Enums;

namespace Adros.Application.DTOs.Stage
{
    public class StageEntityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string ImagePath { get; set; } = default!;
        public StageType? Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public int? Order { get; set; }
        public int TeachersCount { get; set; } 
        public int StudentsCount { get; set; }

    }
}
