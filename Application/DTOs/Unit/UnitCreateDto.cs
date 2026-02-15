using Adros.Application.DTOs.Lesson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Topic
{
    public class UnitCreateDto
    {
        [Required]
        public string Title { get; set; } = default!;
        [Required]
        public Guid SubjectId { get; set; }
        public string Description { get; set; } = string.Empty;
        //public List<LessonCreateDto>? Lessons { get; set; } = new List<LessonCreateDto>();
    }
}
