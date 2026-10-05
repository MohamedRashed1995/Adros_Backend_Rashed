using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs.Level
{
    public class LevelCreateDto
    {
        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public Guid StageId { get; set; }
    }
}
