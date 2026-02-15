using Adros.Application.DTOs.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Lesson
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        //public string LessonfileName { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int? Duration { get; set; }
        public Guid? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string AboutTeacher { get; set; }

        //public Guid? SubjectId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ExamId { get; set; }
        public string? ExamTitle { get; set; }

        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
        public List<LessonVideoDto> Videos { get; set; } = new List<LessonVideoDto>();
    }

}
