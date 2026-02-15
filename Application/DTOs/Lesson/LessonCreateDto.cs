using Adros.Application.ValidationAttributes;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Users;
using Adros.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Lesson
{
    public class LessonCreateDto
    {
        //public IFormFile? Lessonfile { get; set; }
        //public string LessonfileName { get; set; }
        public int Order { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid TeacherId { get; set; }
        
        public Guid UnitId { get; set; }
        //public Guid? ExamId { get; set; }
    }


}
