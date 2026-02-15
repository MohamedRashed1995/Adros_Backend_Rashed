using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs
{
    public class LoginDto
    {
        //[DataType(DataType.PhoneNumber)]
        //public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
