using Adros.Core.Entities.Course;
<<<<<<< HEAD
using Adros.Core.Entities.Home;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Adros.Core.Entities.Users
{
    public class Teacher : BaseEntity
    {
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
<<<<<<< HEAD
        public string Email { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string phoneNumber { get; set; } = default!;
        public string About { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string ? ProfilePictureUrl { get; set; }
        public Guid? StageId { get; set; }
        public Stage? Stage { get; set; }
=======
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; } = default!;
        
        
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    }
}
