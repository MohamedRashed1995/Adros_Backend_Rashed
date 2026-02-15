using Adros.Core.Entities.Users;
using Adros.Shared;
using Microsoft.AspNetCore.Identity;
namespace Adros.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public string? ResetPasswordCode { get; set; }
        public DateTime? ResetPasswordCodeExpiry { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public Guid? StudentId { get; set; }
        public Student? Student { get; set; }
        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<UserOtp> UserOtps { get; set; } = [];

        public bool IsActive { get; set; } = true;

    }
}
