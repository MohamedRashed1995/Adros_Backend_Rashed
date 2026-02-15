using Adros.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Lesson
{
    public class LessonEntityDto : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Order { get; set; }
    }
}
