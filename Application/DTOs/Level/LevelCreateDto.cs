using Adros.Application.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs.Level
{
    public class LevelCreateDto
    {
        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public Guid StageId { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5MB.")]
        [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif"], ErrorMessage = "Only .jpg, .jpeg, .png, and .gif files are allowed.")]
        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
    }
}
