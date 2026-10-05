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
    public class TeacherUpdateDto
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? About { get; set; }

        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5MB.")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" }, ErrorMessage = "Only .jpg, .jpeg, .png, and .gif files are allowed.")]
        [DataType(DataType.Upload)]
        public IFormFile? Photo { get; set; }
    }
}
