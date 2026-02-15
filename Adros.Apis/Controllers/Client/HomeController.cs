using Adros.Apis.ApiResponse;
<<<<<<< HEAD
using Adros.Application.DTOs.Calender;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Application.DTOs.Home;
using Adros.Application.DTOs.Level;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
<<<<<<< HEAD
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
            var StudentId = await _studentService.GetStudentIdByUserIdAsync(userId);



            //var studentId = await _sharedUserService.GetUserBYId(userId);
            if (StudentId == Guid.Empty)
            {
                return NotFound(new ApiResponse<string>(
                    404, "Student not found", "No student linked to this user."));
            }

            var student = await _studentService.GetStudentProfileAsync(_currentUserService.UserId);
            if (student == null)
            {
                return NotFound(new ApiResponse<string>(
                    404, "Student profile not found", $"No profile for studentId {StudentId}."));
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
                calenders = calenders,
                IsSubscribed = student.IsSubscribed
            };

            _logger.LogInformation("{Operation} completed successfully", operation);
            return Ok(new ApiResponse<HomeResponseDto>(
                200, "Home content retrieved successfully", response));
        }
    }
}
=======
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Client
{
    /// <summary>
    /// Controller for handling home page related operations
    /// </summary>
    /// 

    [ApiController]
    [Route("api/[controller]")]
    public class HomeController(
        IBannerService bannerService,
        IStageService stageService,
        ICalenderService calenderService,
        ILevelService levelService,
        ICurrentUserService currentUserService,
        IStudentService studentService,
        ISharedUserService sharedUserService,
        ILogger<HomeController> logger) : BaseApiController
    {
        private readonly IBannerService _bannerService = bannerService;
        private readonly IStageService _stageService = stageService;
        private readonly ICalenderService _calenderService = calenderService;
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ILevelService _levelService = levelService;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IStudentService _studentService = studentService;
        private readonly ISharedUserService _sharedUserService = sharedUserService;
        /// <summary>
        /// Retrieves home page content including banners, stages, and calendar events
        /// </summary>
        /// <param name="startDate">Optional start date for calendar events filter (UTC)</param>
        /// <param name="endDate">Optional end date for calendar events filter (UTC)</param>
        /// <returns>Complete home page content</returns>
        /// <response code="200">Returns home page content</response>
        /// <response code="400">Invalid date range parameters</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<HomeResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HomeResponseDto>> GetHomeContentAsync(
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null)
        {
            const string operation = nameof(GetHomeContentAsync);

            try
            {
                _logger.LogInformation("{Operation} initiated", operation);
                var userId = _currentUserService.UserId;

                // 1️⃣ جلب studentId
                var studentId = await _sharedUserService.GetStudentIdByUserIdAsync(userId);
                if (!studentId.HasValue)
                {
                    _logger.LogWarning("User {UserId} is not associated with a student", userId);
                    return BadRequest(new ApiResponse<string>(
                        400,
                        "Student not found",
                        "User is not associated with a student"
                    ));
                }

                // 2️⃣ جلب بيانات الطالب
                var student = await _studentService.GetStudentProfileAsync(studentId.Value);

                // 3️⃣ التحقق من Level
                if (!student.LevelId.HasValue)
                {
                    _logger.LogWarning("Student {StudentId} does not have a level assigned", studentId);
                    return BadRequest(new ApiResponse<string>(
                        400,
                        "Level not assigned",
                        "Student does not have a level assigned"
                    ));
                }

                var levelId = student.LevelId.Value;

                // 4️⃣ جلب Level مع Stage
                var level = await _levelService.GetLevelByIdAsync(levelId);
                if (level == null)
                {
                    _logger.LogWarning("Level {LevelId} not found for student {StudentId}", levelId, studentId);
                    return BadRequest(new ApiResponse<string>(
                        400,
                        "Level not found",
                        "Assigned level does not exist"
                    ));
                }

                _logger.LogInformation("Level {LevelId} retrieved successfully", levelId);

                // 5️⃣ التحقق من التاريخ
                if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                {
                    _logger.LogWarning("Invalid date range: {StartDate} to {EndDate}", startDate, endDate);
                    return BadRequest(new ApiResponse<string>(
                        400,
                        "Invalid date range",
                        "Start date must be before end date"));
                }

                // 6️⃣ جلب باقي البيانات
                var banners = await _bannerService.GetClientBannersAsync();
                var calenders = await _calenderService.GetClientCalendersAsync();

                var response = new HomeResponseDto
                {
                    Banners = banners,
                    levels = level,
                    calenders = calenders
                };

                _logger.LogInformation("{Operation} completed successfully", operation);

                return Ok(new ApiResponse<HomeResponseDto>(
                    200,
                    "Home content retrieved successfully",
                    response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Operation} failed", operation);
                return StatusCode(
                    500,
                    new ApiResponse<string>(
                        500,
                        "Error retrieving home content",
                        ex.Message));
            }
        }






    }
}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
