using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Topic
{
    public class UnitUpdateDto
    {
        public string? Title { get; set; }
        public Guid? LessonId { get; set; }
        public string Description { get; set; }
    }
}
