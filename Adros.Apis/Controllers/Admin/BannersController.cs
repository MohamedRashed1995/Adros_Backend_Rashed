using Adros.Application.DTOs.Banner;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using System;
using System.Linq;
using System.Threading.Tasks;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

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

<<<<<<< HEAD
        // =================== GET ALL ===================
=======
        // =================== GET: api/Banners ===================
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            var banners = await _bannerService.GetClientBannersAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Banners retrieved successfully",
<<<<<<< HEAD
                Data = banners.Select(b => new
                {
                    b.Id,
                    b.Order,
                    b.ImageUrl // URL جاهز للعرض على الـ frontend
                })
            });
        }

        // =================== GET BY ID ===================
=======
                Data = banners
            });
        }

        // =================== GET: api/Banners/{id} ===================
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannerById(Guid id)
        {
            var banner = await _bannerService.GetBannerByIdAsync(id);
<<<<<<< HEAD
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
=======
            if (banner == null) return NotFound(new { StatusCode = 404, Message = "Banner not found" });

            return Ok(new { StatusCode = 200, Message = "Banner retrieved successfully", Data = banner });
        }

        // =================== POST: api/Banners ===================
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpPost]
        public async Task<IActionResult> CreateBanner([FromForm] BannerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var banner = await _bannerService.CreateBannerAsync(dto);
<<<<<<< HEAD
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
=======
            return Ok(new { StatusCode = 200, Message = "Banner created successfully", Data = banner });
        }

        // =================== PUT: api/Banners/{id} ===================
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(Guid id, [FromForm] BannerUpdateDto dto)
        {
            var banner = await _bannerService.UpdateBannerAsync(id, dto);
<<<<<<< HEAD
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

=======
            if (banner == null) return NotFound(new { StatusCode = 404, Message = "Banner not found" });

            return Ok(new { StatusCode = 200, Message = "Banner updated successfully", Data = banner });
        }

        // =================== DELETE: api/Banners/{id} ===================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(Guid id)
        {
            await _bannerService.DeleteBannerAsync(id);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            return Ok(new { StatusCode = 200, Message = "Banner deleted successfully" });
        }
    }
}
