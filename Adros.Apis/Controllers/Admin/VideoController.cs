using Adros.Core.Entities.Course;
using Adros.Core.Enums;
//using Adros.Infrastructure.Data;
using Adros.Infrastructure.External;
using Adros.Persistence.Contexts;

//using Adros.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Admin
{

    public class VideoController(BunnyVideoService bunnyService, AppDbContext context) : BaseApiController
    {
        private readonly BunnyVideoService _bunnyService = bunnyService;
        private readonly AppDbContext _context = context;
        public string libId = "378479";
        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo(IFormFile file, [FromQuery] string title, [FromQuery] Guid topicId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var videoId = await _bunnyService.UploadVideoAsync(filePath, title);
            if (videoId == null)
                return StatusCode(500, "Video upload failed.");

            var video = new Video
            {
                Title = title,
                TopicId = topicId,
                BunnyVideoId = videoId,
                Status = VideoStatus.Processing,
                Url = $"https://iframe.mediadelivery.net/embed/{libId}/{videoId}",
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse("0a44a9e2-906f-4c17-bc16-a68feee2383a"),
                Duration = new TimeOnly(0, 0, 0)
            };
            
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            return Ok(new { videoId });
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> GetVideoUrl(string videoId)
        {
            var videoUrl = await _bunnyService.GetVideoUrlAsync(videoId);
            return videoUrl != null ? Ok(new { url = videoUrl }) : NotFound();
        }
    }
}
