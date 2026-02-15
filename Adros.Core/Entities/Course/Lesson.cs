using Adros.Core.Entities.Assessements;
using Adros.Core.Entities.Users;
using Adros.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Adros.Core.Entities.Course
{
    public class Lesson : BaseEntity
    {
        
        //[DataType(DataType.Upload)]
        //public IFormFile Lessonfile { get; set; } = default!;
        //public string LessonfileName { get; set; }
        public int Order { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; } = default!;
        public Guid? ExamId { get; set; }
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public Guid? UnitId { get; set; }
        public virtual Unit? Unit { get; set; }
        public ICollection<Video> Videos { get; set; } = new List<Video>();
        //public virtual Assessment? Exam { get; set; }
    }
    
}
