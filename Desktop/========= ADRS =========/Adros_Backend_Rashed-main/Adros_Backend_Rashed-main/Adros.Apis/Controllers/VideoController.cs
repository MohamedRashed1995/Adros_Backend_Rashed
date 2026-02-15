//using Adros.Application.DTOs.Video;
//using Adros.Application.Interfaces.IService;
//using Adros.Core.Enums;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace Adros.Apis.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VideoController : ControllerBase
//    {
//        private readonly IVideoService _videoService;
//        private readonly ILogger<VideoController> _logger;

//        public VideoController(
//            IVideoService videoService,
//            ILogger<VideoController> logger)
//        {
//            _videoService = videoService;
//            _logger = logger;
//        }

//        // ========== Public Endpoints ==========

//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetAll()
//        {
//            try
//            {
//                var videos = await _videoService.GetAllVideosAsync();
//                return Ok(videos);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error getting all videos");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpGet("{id}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetById(Guid id)
//        {
//            var video = await _videoService.GetVideoByIdAsync(id);
//            return Ok(video);
//        }


//        [HttpGet("lesson/{lessonId}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetByLessonId(Guid lessonId)
//        {
//            try
//            {
//                var videos = await _videoService.GetVideosByLessonIdAsync(lessonId);
//                return Ok(videos);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error getting videos by lesson ID: {LessonId}", lessonId);
//                return StatusCode(500, "Internal server error");
//            }
//        }



//        [HttpGet("{id}/views")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetViewsCount(Guid id)
//        {
//            try
//            {
//                var count = await _videoService.GetVideoViewsCountAsync(id);
//                return Ok(new { videoId = id, viewsCount = count });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error getting video views count: {Id}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpGet("{id}/downloads")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetDownloadsCount(Guid id)
//        {
//            try
//            {
//                var count = await _videoService.GetVideoDownloadsCountAsync(id);
//                return Ok(new { videoId = id, downloadsCount = count });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error getting video downloads count: {Id}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        // ========== Protected Endpoints ==========

//        [HttpPost("{id}/view")]
//        [Authorize]
//        public async Task<IActionResult> IncrementViews(Guid id)
//        {
//            try
//            {
//                var userId = GetUserIdFromToken();
//                await _videoService.IncrementVideoViewsAsync(id, userId);
//                return Ok(new { message = "View recorded successfully" });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error incrementing video views: {Id}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPost("{id}/download")]
//        [Authorize]
//        public async Task<IActionResult> Download(Guid id)
//        {
//            try
//            {
//                var userId = GetUserIdFromToken();
//                var download = await _videoService.DownloadVideoAsync(id, userId);
//                return Ok(download);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error downloading video: {Id}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        // ========== Admin Endpoints ==========

//        [HttpPost]
//        //[Authorize(Roles = "Admin,Teacher")]
//        public async Task<IActionResult> Create([FromBody] CreateVideoDto dto)
//        {
//            try
//            {
//                var video = await _videoService.CreateVideoAsync(dto);
//                return CreatedAtAction(nameof(GetById), new { id = video.Id }, video);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating video");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPut("{id}")]
//        //[Authorize(Roles = "Admin,Teacher")]
//        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVideoDto dto)
//        {
//            try
//            {
//                var video = await _videoService.UpdateVideoAsync(id, dto);
//                return Ok(video);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error updating video: {Id}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpDelete("{id}")]
//        //[Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Delete(Guid id)
//        {
//            try
//            {
//                var result = await _videoService.DeleteVideoAsync(id);
//                if (!result)
//                    return NotFound($"Video with ID {id} not found");

//                return Ok(new { message = "Video deleted successfully" });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error deleting video: {Id}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPatch("{id}/status")]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] VideoStatus status)
//        {
//            try
//            {
//                var result = await _videoService.ChangeVideoStatusAsync(id, status);
//                if (!result)
//                    return NotFound($"Video with ID {id} not found");

//                return Ok(new { message = $"Video status changed to {status}" });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error changing video status: {Id}, Status: {Status}", id, status);
//                return StatusCode(500, "Internal server error");
//            }
//        }


//        // ========== Helper Methods ==========

//        private Guid GetUserIdFromToken()
//        {
//            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (string.IsNullOrEmpty(userIdClaim))
//                throw new UnauthorizedAccessException("Invalid token");

//            return Guid.Parse(userIdClaim);
//        }
//    }
//}


using Adros.Application.DTOs.Video;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Course;
using Adros.Core.Enums;
using Adros.Infrastructure.External;
using Adros.Persistence.Contexts;
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Adros.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly BunnyVideoService _bunnyService;
        private readonly AppDbContext _context;
        private readonly ILogger<VideoController> _logger;
        private readonly IWebHostEnvironment _env;
        private const string LibraryId = "378479";

        public VideoController(
            IVideoService videoService,
            BunnyVideoService bunnyService,
            AppDbContext context,
            ILogger<VideoController> logger,
            IWebHostEnvironment env)
        {
            _videoService = videoService;
            _bunnyService = bunnyService;
            _context = context;
            _logger = logger;
            _env = env;
        }

        // ================= PUBLIC =================

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var videos = await _videoService.GetAllVideosAsync();
            return Ok(videos);
        }

        [HttpGet("{videoId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid videoId)
        {
            var video = await _videoService.GetVideoByIdAsync(videoId);
            return Ok(video);
        }


        [HttpGet("lesson/{lessonId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByLessonId(Guid lessonId)
        {
            var videos = await _videoService.GetVideosByLessonIdAsync(lessonId);
            return Ok(videos);
        }

        [HttpGet("{id}/views")]
        [AllowAnonymous]
        public async Task<IActionResult> GetViews(Guid id)
        {
            var count = await _videoService.GetVideoViewsCountAsync(id);
            return Ok(new { videoId = id, viewsCount = count });
        }

        [HttpGet("{id}/downloads")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDownloads(Guid id)
        {
            var count = await _videoService.GetVideoDownloadsCountAsync(id);
            return Ok(new { videoId = id, downloadsCount = count });
        }

        // ================= STUDENT =================

        [HttpPost("{id}/view")]
        //[Authorize]
        public async Task<IActionResult> AddView(Guid id)
        {
            var userId = GetUserId();
            await _videoService.IncrementVideoViewsAsync(id, userId);
            return Ok(new { message = "View recorded" });
        }

        [HttpPost("{id}/download")]
        [Authorize]
        public async Task<IActionResult> Download(Guid id)
        {
            var userId = GetUserId();
            var result = await _videoService.DownloadVideoAsync(id, userId);
            return Ok(result);
        }

        // ================= ADMIN / TEACHER =================

        [HttpPost("upload")]
        //[Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UploadVideo(
    IFormFile file,
    [FromQuery] string title,
    [FromQuery] Guid unitId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            // ===== Save temp file =====
            var tempPath = Path.GetTempFileName();
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // ===== Upload to Bunny =====
            var bunnyVideoId = await _bunnyService.UploadVideoAsync(tempPath, title);
            if (string.IsNullOrWhiteSpace(bunnyVideoId))
                return StatusCode(500, "Bunny upload failed");

            // ===== Create DTO =====
            var dto = new CreateVideoDto
            {
                Title = title,
                UnitId = unitId,
                LessonId = Guid.Empty, // ⚠️ لو عندك LessonId ابعته هنا
                Url = $"https://iframe.mediadelivery.net/embed/{LibraryId}/{bunnyVideoId}",
                Description = null,
                Duration = null,
                ThumbnailUrl = null
            };

            // ===== Save via Service (ONLY) =====
            var result = await _videoService.CreateVideoAsync(
                dto,
                VideoSourceType.Upload
            );

            return Ok(result);
        }

        [Authorize(Roles = SystemRoles.Teacher)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateVideoRequest request)
        {
            try
            {
                if (request.VideoFile == null && string.IsNullOrWhiteSpace(request.VideoUrl))
                    return BadRequest("You must upload a video OR provide a video link.");

                if (request.VideoFile != null && !string.IsNullOrWhiteSpace(request.VideoUrl))
                    return BadRequest("Choose only one: upload OR link.");

                string finalVideoUrl;
                string? duration = null;
                VideoSourceType sourceType;

                // ===== CASE 1: Upload Video =====
                if (request.VideoFile != null)
                {
                    sourceType = VideoSourceType.Upload;

                    var videoFileName = await FileManager.UploadVideoAsync(request.VideoFile);

                    // ✅ URL صح
                    finalVideoUrl = $"/Uploads/Videos/{videoFileName}";

                    // ✅ path صح على السيرفر
                    var videoPath = Path.Combine(
                        _env.WebRootPath,
                        "Uploads",
                        "Videos",
                        videoFileName
                    );

                    var ffprobePath = Path.Combine(
                        _env.ContentRootPath,
                        "ffmpeg",
                        "bin",
                        "ffprobe.exe"
                    );

                    if (System.IO.File.Exists(ffprobePath) && System.IO.File.Exists(videoPath))
                    {
                        duration = VideoHelper.GetVideoDuration(videoPath, ffprobePath);
                    }
                }
                else
                {
                    sourceType = VideoSourceType.ExternalLink;
                    finalVideoUrl = request.VideoUrl!.Trim();
                }

                // ===== Thumbnail Image =====
                string? imageUrl = null;
                if (request.ImageFile != null)
                {
                    var imageFileName = await FileManager.UploadImageAsync(request.ImageFile);

                    // ✅ URL صح
                    imageUrl = $"/Uploads/Images/{imageFileName}";
                }

                var dto = new CreateVideoDto
                {
                    Title = request.Title,
                    Description = request.Description,
                    Duration = duration,
                    LessonId = request.LessonId,
                    UnitId = request.UnitId,
                    Url = finalVideoUrl,
                    ThumbnailUrl = imageUrl
                };

                var result = await _videoService.CreateVideoAsync(dto, sourceType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating video");
                return StatusCode(500, new
                {
                    message = "Internal server error",
                    detail = ex.Message
                });
            }
        }










        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(Guid id, UpdateVideoDto dto)
        {
            var video = await _videoService.UpdateVideoAsync(id, dto);
            return Ok(video);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _videoService.DeleteVideoAsync(id);
            return result ? Ok() : NotFound();
        }

        [HttpPatch("{id}/status")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] VideoStatus status)
        {
            var result = await _videoService.ChangeVideoStatusAsync(id, status);
            return result ? Ok() : NotFound();
        }

        // ================= HELPER =================

        private Guid GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(claim))
                throw new UnauthorizedAccessException();

            return Guid.Parse(claim);
        }
    }
}
