using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs
{
    public class RegisterDto
    {


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Email { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Compare("Password")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
