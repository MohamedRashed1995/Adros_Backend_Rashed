using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Lesson
{
    public class UnitWithLessonsDto
    {
        public Guid UnitId { get; set; }
        public string UnitTitle { get; set; }
        public string UnitDescription { get; set; }

        public List<LessonEntityDto> Lessons { get; set; }
    }

}
