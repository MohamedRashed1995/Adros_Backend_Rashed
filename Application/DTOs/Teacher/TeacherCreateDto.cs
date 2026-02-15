using Adros.Application.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Teacher
{
    public class TeacherCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;

        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5MB.")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" }, ErrorMessage = "Only .jpg, .jpeg, .png, and .gif files are allowed.")]
        [DataType(DataType.Upload)]
        public IFormFile? Photo { get; set; }
        [Required]
        public Guid StageId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
