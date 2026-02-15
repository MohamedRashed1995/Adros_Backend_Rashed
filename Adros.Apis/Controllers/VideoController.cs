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

<<<<<<< HEAD

using Adros.Apis.Helpers;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Application.DTOs.Video;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Course;
using Adros.Core.Enums;
using Adros.Infrastructure.External;
using Adros.Persistence.Contexts;
<<<<<<< HEAD
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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
<<<<<<< HEAD
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _env;
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        private const string LibraryId = "378479";

        public VideoController(
            IVideoService videoService,
            BunnyVideoService bunnyService,
            AppDbContext context,
<<<<<<< HEAD
            ILogger<VideoController> logger,
            IStudentService studentService,
            IWebHostEnvironment env)
=======
            ILogger<VideoController> logger)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        {
            _videoService = videoService;
            _bunnyService = bunnyService;
            _context = context;
            _logger = logger;
<<<<<<< HEAD
            _studentService = studentService;
            _env = env;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        }

        // ================= PUBLIC =================

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
<<<<<<< HEAD
            try
            {
                var videos = await _videoService.GetAllVideosAsync();
                return Ok(videos);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting all videos");
                return StatusCode(500, new
                {
                    message = ex.Message,
                    stack = ex.StackTrace,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("{videoId}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetById(Guid videoId)
        {
            var video = await _videoService.GetVideoByIdAsync(videoId);
            return Ok(video);
        }


=======
            var videos = await _videoService.GetAllVideosAsync();
            return Ok(videos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var video = await _videoService.GetVideoByIdAsync(id);
            return Ok(video);
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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
<<<<<<< HEAD
            var count = await _videoService.GetVideoViewsCountForCurrentStudentAsync(id);
            return Ok(new { videoId = id, viewsCount = count });
        }

        // ================= STUDENT =================

        [HttpPost("{videoId}/view")]
        [Authorize]
        public async Task<IActionResult> AddView(Guid videoId, [FromBody] VideoViewRequest request)
        {
            var userId = GetUserId();
            var studentId = await _studentService.GetStudentIdByUserIdAsync(userId);

            await _videoService.RecordVideoViewAsync(videoId, studentId, request.WatchedSeconds);

            return Ok();
        }


        [HttpGet("{videoId}/MyViews")]
        public async Task<IActionResult> GetMyViews(Guid videoId)
        {
            var count = await _videoService.GetVideoViewsCountForCurrentStudentAsync(videoId);
            return Ok(new { ViewsCount = count });
        }
        // ================= ADMIN / TEACHER =================

        //[HttpPost("upload")]
        ////[Authorize(Roles = "Admin,Teacher")]
        //public async Task<IActionResult> UploadVideo(IFormFile file,[FromQuery] string title,[FromQuery] Guid unitId)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("Invalid file");

        //    // ===== Save temp file =====
        //    var tempPath = Path.GetTempFileName();
        //    using (var stream = new FileStream(tempPath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    // ===== Upload to Bunny =====
        //    var bunnyVideoId = await _bunnyService.UploadVideoAsync(tempPath, title);
        //    if (string.IsNullOrWhiteSpace(bunnyVideoId))
        //        return StatusCode(500, "Bunny upload failed");

        //    // ===== Create DTO =====
        //    var dto = new CreateVideoDto
        //    {
        //        Title = title,
        //        UnitId = unitId,
        //        LessonId = Guid.Empty, // ⚠️ لو عندك LessonId ابعته هنا
        //        Url = $"https://iframe.mediadelivery.net/embed/{LibraryId}/{bunnyVideoId}",
        //        Description = null,
        //        Duration = null,
        //        ThumbnailUrl = null
        //    };

        //    // ===== Save via Service (ONLY) =====
        //    var result = await _videoService.CreateVideoAsync(
        //        dto,
        //        VideoSourceType.Upload
        //    );

        //    return Ok(result);
        //}

        //[Authorize(Roles = SystemRoles.Teacher)]
        //[Authorize(Roles = SystemRoles.Teacher)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateVideoRequest request)
        {
            try
            {
                
                string finalVideoUrl;
                
                VideoSourceType sourceType;

                
                    sourceType = VideoSourceType.ExternalLink;
                    finalVideoUrl = request.VideoUrl!;
                
                if (finalVideoUrl.Contains("vimeo.com"))
                {
                    var dto = new CreateVideoDto
                    {
                        Title = request.Title,
                        Description = request.Description,
                        LessonId = request.LessonId,
                        UnitId = request.UnitId,
                        Url = finalVideoUrl,
                        
                    };
                    var result = await _videoService.CreateVideoAsync(dto, sourceType);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Please provide a valid Vimeo link.");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create Video Error: {@Request}", request);
                return StatusCode(500, new
                {
                    message = ex.Message,
                    stack = ex.StackTrace,
                    inner = ex.InnerException?.Message
                });
            }

        }



=======
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
        [Authorize]
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

            var tempPath = Path.GetTempFileName();
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var bunnyVideoId = await _bunnyService.UploadVideoAsync(tempPath, title);
            if (bunnyVideoId == null)
                return StatusCode(500, "Bunny upload failed");

            var video = new Video
            {
                Id = Guid.NewGuid(),
                Title = title,
                UnitId = unitId,
                BunnyVideoId = bunnyVideoId,
                Status = VideoStatus.Processing,
                Url = $"https://iframe.mediadelivery.net/embed/{LibraryId}/{bunnyVideoId}",
                CreatedAt = DateTime.UtcNow
            };

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            return Ok(new { video.Id, video.Url });
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(CreateVideoDto dto)
        {
            var video = await _videoService.CreateVideoAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = video.Id }, video);
        }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

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

<<<<<<< HEAD
        [HttpPost("{VideoId}/watchlater")]
        [Authorize(Roles = SystemRoles.Student)]
        public async Task<IActionResult> ToggleWatchLater(Guid VideoId)
        {
            try
            {
                var UserId = GetUserId();

                var studentId = await _studentService.GetStudentIdByUserIdAsync(UserId);


                var result = await _videoService.ToggleWatchLaterAsync(VideoId, studentId);

                return Ok(new
                {
                    videoId = VideoId,
                    watchLater = result // true → تمت الإضافة, false → تم الإلغاء
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("watchlater")]
        [Authorize(Roles = SystemRoles.Student)]
        public async Task<IActionResult> GetMyWatchLaterVideos()
        {
            try
            {
                var userId = GetUserId();
                var studentId = await _studentService.GetStudentIdByUserIdAsync(userId);
                var student = await _studentService.GetStudentByIdAsync(studentId); 
                if (student == null)
                    return NotFound("Student not found");
                


                
                if (student.IsSubscriped == true)
                {
                    var videos = await _videoService.GetWatchLaterVideosAsync(studentId);
                    return Ok(videos);
                }
                else
                {
                    return BadRequest("Student has no subscribtions");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching watch later videos");
                return StatusCode(500, ex.Message);
            }
        }




=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpPatch("{id}/status")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] VideoStatus status)
        {
            var result = await _videoService.ChangeVideoStatusAsync(id, status);
            return result ? Ok() : NotFound();
        }

        // ================= HELPER =================
<<<<<<< HEAD
        
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        private Guid GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(claim))
                throw new UnauthorizedAccessException();

            return Guid.Parse(claim);
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
