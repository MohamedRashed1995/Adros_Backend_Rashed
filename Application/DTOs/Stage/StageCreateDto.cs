using Adros.Application.ValidationAttributes;
using Adros.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Stage
{
    public class StageCreateDto
    {
        public string Title { get; set; } = default!;
        [Required(ErrorMessage = "Image is required.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5MB.")]
        [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif"], ErrorMessage = "Only .jpg, .jpeg, .png, and .gif files are allowed.")]
        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
        [Range(1, int.MaxValue)]
        public int? Order { get; set; }
        public StageType? Type { get; set; }
    }
}
