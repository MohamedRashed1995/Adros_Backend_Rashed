using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adros.Application.DTOs.Lesson;
using Adros.Shared;

namespace Adros.Application.DTOs.Topic
{
    public class UnitEntityDto: BaseEntity
    {
        //public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public Guid SubjectId { get; set; }
<<<<<<< HEAD
        public string Description { get; set; } = string.Empty;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        //public List<LessonEntityDto> Lessons { get; set; } = new List<LessonEntityDto>();
    }
}
