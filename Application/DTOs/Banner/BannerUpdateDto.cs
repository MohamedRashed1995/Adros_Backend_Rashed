using Adros.Shared;
using Microsoft.AspNetCore.Http;

namespace Adros.Application.DTOs.Banner
{
    //public class BannerUpdateDto : BaseEntity
    //{
    //    //public Guid Id { get; set; }
    //    public IFormFile? Image { get; set; }
    //    public int? Order { get; set; }
    //}
    public class BannerUpdateDto
    {
        //public string? Title { get; set; }
        public int? Order { get; set; }
        public IFormFile? Image { get; set; }
    }
}
