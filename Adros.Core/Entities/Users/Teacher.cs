using Adros.Core.Entities.Course;
using Adros.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Adros.Core.Entities.Users
{
    public class Teacher : BaseEntity
    {
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid TeacherID { get; set; }
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; } = default!;
        
        //[DataType(DataType.Upload)]
        //public IFormFile? Photo { get; set; }
        public ICollection<Lesson> Lessons { get; set; } = [];

    }
}
