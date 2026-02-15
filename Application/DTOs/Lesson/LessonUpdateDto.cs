using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs.Lesson
{
    public class LessonUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public int Order { get; set; }

        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        public Guid TeacherId { get; set; }

        public Guid? ExamId { get; set; }

        // optional new file
<<<<<<< HEAD
        //public IFormFile? Lessonfile { get; set; }
=======
        public IFormFile? Lessonfile { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
