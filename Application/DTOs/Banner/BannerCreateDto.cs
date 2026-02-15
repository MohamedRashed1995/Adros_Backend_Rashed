using Adros.Application.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs.Banner
{
    //public class BannerCreateDto
    //{
    //    [Required(ErrorMessage = "Image is required.")]
    //    [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5MB.")]
    //    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif"], ErrorMessage = "Only .jpg, .jpeg, .png, and .gif files are allowed.")]
    //    [DataType(DataType.Upload)]
    //    public IFormFile Image { get; set; } = default!;
    //    //public string ImageUrl { get; set; } = default!;
    //    [Range(1,int.MaxValue)]
    //    public int? Order { get; set; }
    //}
    public class BannerCreateDto
    {
        //public string Title { get; set; }
        public int Order { get; set; }
        public IFormFile? Image { get; set; }
    }
}
