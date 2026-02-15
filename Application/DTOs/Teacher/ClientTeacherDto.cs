using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Teacher
{
    public class ClientTeacherDto
    {
        public Guid TeacherId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public int LessonCount { get; set; }
    }
}
