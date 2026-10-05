using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Teacher
{
    public class TeacherChangePasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
