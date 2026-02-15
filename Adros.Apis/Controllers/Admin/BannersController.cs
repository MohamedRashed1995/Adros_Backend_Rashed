using Adros.Application.DTOs.Banner;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Adros.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        // =================== GET ALL ===================
        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            var banners = await _bannerService.GetClientBannersAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Banners retrieved successfully",
                Data = banners.Select(b => new
                {
                    b.Id,
                    b.Order,
                    b.ImageUrl // URL جاهز للعرض على الـ frontend
                })
            });
        }

        // =================== GET BY ID ===================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannerById(Guid id)
        {
            var banner = await _bannerService.GetBannerByIdAsync(id);
            if (banner == null)
                return NotFound(new { StatusCode = 404, Message = "Banner not found" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Banner retrieved successfully",
                Data = new
                {
                    banner.Id,
                    banner.Order,
                    banner.ImageUrl
                }
            });
        }

        // =================== CREATE ===================
        [HttpPost]
        public async Task<IActionResult> CreateBanner([FromForm] BannerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var banner = await _bannerService.CreateBannerAsync(dto);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Banner created successfully",
                Data = new
                {
                    banner.Id,
                    banner.Order,
                    banner.ImageUrl
                }
            });
        }

        // =================== UPDATE ===================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(Guid id, [FromForm] BannerUpdateDto dto)
        {
            var banner = await _bannerService.UpdateBannerAsync(id, dto);
            if (banner == null)
                return NotFound(new { StatusCode = 404, Message = "Banner not found" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Banner updated successfully",
                Data = new
                {
                    banner.Id,
                    banner.Order,
                    banner.ImageUrl
                }
            });
        }

        // =================== DELETE ===================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(Guid id)
        {
            var deleted = await _bannerService.DeleteBannerAsync(id);
            if (!deleted)
                return NotFound(new { StatusCode = 404, Message = "Banner not found" });

            return Ok(new { StatusCode = 200, Message = "Banner deleted successfully" });
        }
    }
}
