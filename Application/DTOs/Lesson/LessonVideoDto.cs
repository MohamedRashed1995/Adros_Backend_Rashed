using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Lesson
{
    public class LessonVideoDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string? VimeoId { get; set; }
        public string VideoName { get; set; }
        public string VideoTeacher { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        //public string? ThumbnailUrl { get; set; }
        public bool isWatchlater { get; set; }
        //public List<string> Attachments { get; set; } = new();
    }
}
