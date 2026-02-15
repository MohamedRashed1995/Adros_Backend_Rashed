<<<<<<< HEAD
﻿using Adros.Application.DTOs.Attachment;
using System;
=======
﻿using System;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Lesson
{
    public class LessonDto
    {
        public Guid Id { get; set; }
<<<<<<< HEAD
        //public string LessonfileName { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int? Duration { get; set; }
        public Guid? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string AboutTeacher { get; set; }
=======
        public string LessonfileName { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid? TeacherId { get; set; }
        public string TeacherName { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

        //public Guid? SubjectId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ExamId { get; set; }
        public string? ExamTitle { get; set; }

<<<<<<< HEAD
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
        public List<LessonVideoDto> Videos { get; set; } = new List<LessonVideoDto>();
=======
        public List<string> Attachments { get; set; } = new();
        public List<string> Videos { get; set; } = new();
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }

}
