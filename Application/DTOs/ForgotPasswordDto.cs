using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Email Address is in correct")]
        public string Email { get; set; } = default!;
    }
    
}
