using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Calender;
using Adros.Application.DTOs.Home;
using Adros.Application.DTOs.Level;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        private readonly ICalenderService _calenderService;
        private readonly ILevelService _levelService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStudentService _studentService;
        private readonly ISharedUserService _sharedUserService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IBannerService bannerService,
            ICalenderService calenderService,
            ILevelService levelService,
            ICurrentUserService currentUserService,
            IStudentService studentService,
            ISharedUserService sharedUserService,
            ILogger<HomeController> logger)
        {
            _bannerService = bannerService;
            _calenderService = calenderService;
            _levelService = levelService;
            _currentUserService = currentUserService;
            _studentService = studentService;
            _sharedUserService = sharedUserService;
            _logger = logger;
        }

        [HttpGet("GetHome")]
        public async Task<IActionResult> GetHomeContentAsync([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var operation = nameof(GetHomeContentAsync);
            _logger.LogInformation("{Operation} started", operation);

            // 🔹 Check if HttpContext and User are available
            if (HttpContext?.User == null)
            {
                return Unauthorized(new ApiResponse<string>(
                    401, "Unauthorized", "No user info found in request."));
            }

            var userId = _currentUserService.UserId;

            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<string>(
                    401, "Unauthorized", "Invalid token or missing NameIdentifier/userId claim."));
            }

            // 🔹 Get student linked to ApplicationUserId
            var studentId = await _sharedUserService.GetUserBYId(userId);
            if (studentId.Id==null)
            {
                return NotFound(new ApiResponse<string>(
                    404, "Student not found", "No student linked to this user."));
            }

            var student = await _studentService.GetStudentProfileAsync(studentId.Id);
            if (student == null)
            {
                return NotFound(new ApiResponse<string>(
                    404, "Student profile not found", $"No profile for studentId {studentId.Id}."));
            }

            // 🔹 Get Level if exists
            LevelEntityDto? level = null;
            if (student.LevelId.HasValue)
            {
                level = await _levelService.GetLevelByIdAsync(student.LevelId.Value);
            }

            // 🔹 Get banners and calendars
            var banners = await _bannerService.GetClientBannersAsync();
            var calenders = await _calenderService.GetClientCalendersAsync(userId, startDate, endDate)
                            ?? new List<ClientCalenderDto>();

            var response = new HomeResponseDto
            {
                Banners = banners,
                levels = level,
                calenders = calenders
            };

            _logger.LogInformation("{Operation} completed successfully", operation);
            return Ok(new ApiResponse<HomeResponseDto>(
                200, "Home content retrieved successfully", response));
        }
    }
}
