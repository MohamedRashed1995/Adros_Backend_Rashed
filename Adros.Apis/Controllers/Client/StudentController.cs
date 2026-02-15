////using Adros.Apis.ApiResponse;
////using Adros.Application.DTOs.Level;
////using Adros.Application.DTOs.Student;
////using Adros.Application.Interfaces.IService;
////using Adros.Core.DomainServices;
////using Adros.Core.DomainServices.IDomainService;
////using Adros.Shared.Exceptions;
////using Microsoft.AspNetCore.Authorization;
////using Microsoft.AspNetCore.Mvc;
////using System.Net;
////using System.Security.Claims;

////namespace Adros.Apis.Controllers.Client
////{
////    [ApiController]
////    [Route("api/[controller]")]
////    [Authorize]
////    public class StudentController : ControllerBase
////    {
////        private readonly IStudentService _studentService;
////        private readonly ICurrentUserService _currentUserService;
////        private readonly ILevelService _levelService;
////        private readonly ILogger<StudentController> _logger;

////        public StudentController(
////            IStudentService studentService,
////            ICurrentUserService currentUserService,
////            ILevelService levelService,
////            ILogger<StudentController> logger)
////        {
////            _studentService = studentService;
////            _currentUserService = currentUserService;
////            _levelService = levelService;
////            _logger = logger;
////        }

////        /// <summary>
////        /// الحصول على ملف الطالب الشخصي (يتطلب تسجيل الدخول)
////        /// </summary>
////        /// <returns>بيانات ملف الطالب</returns>
////        /// 

////        [HttpGet("profile")]
////        //[AllowAnonymous]
////        [Authorize(Roles = "Student")]
////        [ProducesResponseType(typeof(ApiResponse<StudentProfileDto>), StatusCodes.Status200OK)]
////        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
////        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
////        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
////        public async Task<IActionResult> GetStudentProfile()
////        {
////            try
////            {
////                _logger.LogInformation($"محاولة جلب ملف الطالب للمستخدم: {User.Identity.Name}");

////                // طريقة 1: من الـ Claims مباشرة
////                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
////                //var userIdClaim = _currentUserService.UserId;
////                if (string.IsNullOrEmpty(userIdClaim))
////                {
////                    return Unauthorized(new ApiResponse<string>(
////                        (int)HttpStatusCode.Unauthorized,
////                        "مستخدم غير معروف",
////                        "User ID not found in token"));
////                }

////                var userId = Guid.Parse(userIdClaim);

////                // طريقة 2: أو من الـ CurrentUserService
////                // var userId = _currentUserService.UserId;

////                _logger.LogInformation($"جلب ملف الطالب للمستخدم: {userId}");

////                var profile = await _studentService.GetStudentProfileAsync(userId);

////                return Ok(new ApiResponse<StudentProfileDto>(
////                    (int)HttpStatusCode.OK,
////                    "تم جلب ملف الطالب بنجاح",
////                    profile));
////            }
////            catch (NotFoundException ex)
////            {
////                _logger.LogWarning(ex, "الطالب غير موجود");
////                return NotFound(new ApiResponse<string>(
////                    (int)HttpStatusCode.NotFound,
////                    ex.Message,
////                    "الطالب غير موجود في النظام"));
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "خطأ في جلب ملف الطالب");
////                return StatusCode((int)HttpStatusCode.InternalServerError,
////                    new ApiResponse<string>(
////                        (int)HttpStatusCode.InternalServerError,
////                        "حدث خطأ في الخادم",
////                        ex.Message));
////            }
////        }

////        /// <summary>
////        /// اختبار مصادقة المستخدم الحالي
////        /// </summary>
////        //[HttpGet("test-auth")]
////        //[Authorize]
////        //public IActionResult TestAuthentication()
////        //{
////        //    try
////        //    {
////        //        var userInfo = new
////        //        {
////        //            IsAuthenticated = _currentUserService.,
////        //            UserId = _currentUserService.UserId,
////        //            UserEmail = _currentUserService.UserEmail,
////        //            UserName = _currentUserService.UserName,
////        //            Message = "المصادقة تعمل بشكل صحيح"
////        //        };

////        //        _logger.LogInformation($"معلومات المستخدم: {System.Text.Json.JsonSerializer.Serialize(userInfo)}");

////        //        return Ok(new ApiResponse<object>(
////        //            (int)HttpStatusCode.OK,
////        //            "اختبار المصادقة ناجح",
////        //            userInfo));
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        _logger.LogError(ex, "خطأ في اختبار المصادقة");
////        //        return StatusCode((int)HttpStatusCode.InternalServerError,
////        //            new ApiResponse<string>(
////        //                (int)HttpStatusCode.InternalServerError,
////        //                "خطأ في اختبار المصادقة",
////        //                ex.Message));
////        //    }
////        //}

////        /// <summary>
////        /// جلب ملف طالب معين (للاختبار فقط - بدون مصادقة)
////        /// </summary>
////        /// <param name="userId">معرف المستخدم</param>
////        //[HttpGet("test/{userId}")]
////        //[AllowAnonymous]
////        //public async Task<IActionResult> TestGetProfile(Guid userId)
////        //{
////        //    try
////        //    {
////        //        _logger.LogInformation($"اختبار جلب ملف الطالب للمستخدم: {userId}");

////        //        var profile = await _studentService.GetStudentProfileAsync(userId);

////        //        return Ok(new ApiResponse<StudentProfileDto>(
////        //            (int)HttpStatusCode.OK,
////        //            "تم جلب ملف الطالب (اختبار)",
////        //            profile));
////        //    }
////        //    catch (NotFoundException ex)
////        //    {
////        //        return NotFound(new ApiResponse<string>(
////        //            (int)HttpStatusCode.NotFound,
////        //            ex.Message,
////        //            "الطالب غير موجود"));
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        _logger.LogError(ex, "خطأ في اختبار جلب الملف");
////        //        return StatusCode((int)HttpStatusCode.InternalServerError,
////        //            new ApiResponse<string>(
////        //                (int)HttpStatusCode.InternalServerError,
////        //                "خطأ في الخادم",
////        //                ex.Message));
////        //    }
////        //}

////        /// <summary>
////        /// جلب المستويات حسب المرحلة
////        /// </summary>
////        /// <param name="stageId">معرف المرحلة</param>
////        [HttpGet("levels/{stageId}")]
////        [ProducesResponseType(typeof(ApiResponse<List<ClientLevelDto>>), (int)HttpStatusCode.OK)]
////        public async Task<IActionResult> GetByStageIdAsync(Guid stageId)
////        {
////            try
////            {
////                var levels = await _levelService.GetLevelsForClientByStageIdAsync(stageId);
////                return Ok(new ApiResponse<List<ClientLevelDto>>(
////                    (int)HttpStatusCode.OK,
////                    "تم جلب المستويات بنجاح",
////                    levels.ToList()));
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "خطأ في جلب المستويات حسب المرحلة");
////                return StatusCode((int)HttpStatusCode.InternalServerError,
////                    new ApiResponse<string>(
////                        (int)HttpStatusCode.InternalServerError,
////                        "خطأ في جلب المستويات",
////                        ex.Message));
////            }
////        }

////        /// <summary>
////        /// معلومات حول API الطالب
////        /// </summary>
////        [HttpGet("info")]
////        //[AllowAnonymous]
////        public IActionResult GetApiInfo()
////        {
////            return Ok(new ApiResponse<object>(
////                (int)HttpStatusCode.OK,
////                "API الطالب يعمل بنجاح",
////                new
////                {
////                    Endpoints = new[]
////                    {
////                        new { Method = "GET", Path = "/api/Student/profile", Description = "جلب ملف الطالب (يتطلب تسجيل دخول)" },
////                        new { Method = "GET", Path = "/api/Student/test-auth", Description = "اختبار المصادقة" },
////                        new { Method = "GET", Path = "/api/Student/levels/{stageId}", Description = "جلب المستويات حسب المرحلة" },
////                        new { Method = "GET", Path = "/api/Student/info", Description = "معلومات حول API" }
////                    },
////                    Version = "1.0",
////                    Status = "Active"
////                }));
////        }
////    }
////}

//using Adros.Apis.ApiResponse;
//using Adros.Application.DTOs.Level;
//using Adros.Application.DTOs.Student;
//using Adros.Application.Interfaces.IService;
//using Adros.Core.DomainServices;
//using Adros.Core.DomainServices.IDomainService;
//using Adros.Shared.Exceptions;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.Security.Claims;

//namespace Adros.Apis.Controllers.Client
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentController : ControllerBase
//    {
//        private readonly IStudentService _studentService;
//        private readonly ICurrentUserService _currentUserService;
//        private readonly ILevelService _levelService;
//        private readonly ILogger<StudentController> _logger;

//        public StudentController(
//            IStudentService studentService,
//            ICurrentUserService currentUserService,
//            ILevelService levelService,
//            ILogger<StudentController> logger)
//        {
//            _studentService = studentService;
//            _currentUserService = currentUserService;
//            _levelService = levelService;
//            _logger = logger;
//        }

//        /// <summary>
//        /// جلب ملف الطالب الشخصي (للطالب فقط)
//        /// </summary>
//        [HttpGet("profile")]
//        [Authorize(Roles = "Student")]
//        [ProducesResponseType(typeof(ApiResponse<StudentProfileDto>), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        public async Task<IActionResult> GetStudentProfile()
//        {
//            try
//            {
//                _logger.LogInformation($"محاولة جلب ملف الطالب للمستخدم: {User.Identity.Name}");

//                // طريقة 1: من الـ Claims مباشرة
//                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                if (string.IsNullOrEmpty(userIdClaim))
//                {
//                    return Unauthorized(new ApiResponse<string>(
//                        (int)HttpStatusCode.Unauthorized,
//                        "مستخدم غير معروف",
//                        "User ID not found in token"));
//                }

//                var userId = Guid.Parse(userIdClaim);

//                // طريقة 2: أو من الـ CurrentUserService
//                // var userId = _currentUserService.UserId;

//                _logger.LogInformation($"جلب ملف الطالب للمستخدم: {userId}");

//                var profile = await _studentService.GetStudentProfileAsync(userId);

//                return Ok(new ApiResponse<StudentProfileDto>(
//                    (int)HttpStatusCode.OK,
//                    "تم جلب ملف الطالب بنجاح",
//                    profile));
//            }
//            catch (NotFoundException ex)
//            {
//                _logger.LogWarning(ex, "الطالب غير موجود");
//                return NotFound(new ApiResponse<string>(
//                    (int)HttpStatusCode.NotFound,
//                    ex.Message,
//                    "الطالب غير موجود في النظام"));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "خطأ في جلب ملف الطالب");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>(
//                        (int)HttpStatusCode.InternalServerError,
//                        "حدث خطأ في الخادم",
//                        ex.Message));
//            }
//        }

//        /// <summary>
//        /// اختبار Authorization والـ Claims
//        /// </summary>
//        [HttpGet("test-auth")]
//        [Authorize]
//        public IActionResult TestAuth()
//        {
//            try
//            {
//                var userInfo = new
//                {
//                    IsAuthenticated = User.Identity?.IsAuthenticated,
//                    UserName = User.Identity?.Name,
//                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
//                    Email = User.FindFirstValue(ClaimTypes.Email),
//                    AllRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(),
//                    IsStudent = User.IsInRole("Student"),
//                    IsTeacher = User.IsInRole("Teacher"),
//                    AllClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
//                };

//                _logger.LogInformation($"معلومات المستخدم: {System.Text.Json.JsonSerializer.Serialize(userInfo)}");

//                return Ok(new ApiResponse<object>(
//                    (int)HttpStatusCode.OK,
//                    "اختبار المصادقة ناجح",
//                    userInfo));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "خطأ في اختبار المصادقة");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>(
//                        (int)HttpStatusCode.InternalServerError,
//                        "خطأ في اختبار المصادقة",
//                        ex.Message));
//            }
//        }

//        /// <summary>
//        /// جلب ملف طالب معين (للاختبار فقط - بدون مصادقة)
//        /// </summary>
//        [HttpGet("test/{userId}")]
//        [AllowAnonymous]
//        public async Task<IActionResult> TestGetProfile(Guid userId)
//        {
//            try
//            {
//                _logger.LogInformation($"اختبار جلب ملف الطالب للمستخدم: {userId}");

//                var profile = await _studentService.GetStudentProfileAsync(userId);

//                return Ok(new ApiResponse<StudentProfileDto>(
//                    (int)HttpStatusCode.OK,
//                    "تم جلب ملف الطالب (اختبار)",
//                    profile));
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(new ApiResponse<string>(
//                    (int)HttpStatusCode.NotFound,
//                    ex.Message,
//                    "الطالب غير موجود"));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "خطأ في اختبار جلب الملف");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>(
//                        (int)HttpStatusCode.InternalServerError,
//                        "خطأ في الخادم",
//                        ex.Message));
//            }
//        }

//        /// <summary>
//        /// جلب المستويات حسب المرحلة
//        /// </summary>
//        [HttpGet("levels/{stageId}")]
//        [Authorize] // أي مستخدم مسجل
//        public async Task<IActionResult> GetLevelsByStageId(Guid stageId)
//        {
//            try
//            {
//                var levels = await _levelService.GetLevelsForClientByStageIdAsync(stageId);
//                return Ok(new ApiResponse<List<ClientLevelDto>>(
//                    (int)HttpStatusCode.OK,
//                    "تم جلب المستويات بنجاح",
//                    levels.ToList()));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "خطأ في جلب المستويات حسب المرحلة");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>(
//                        (int)HttpStatusCode.InternalServerError,
//                        "خطأ في جلب المستويات",
//                        ex.Message));
//            }
//        }

//        /// <summary>
//        /// معلومات حول API الطالب
//        /// </summary>
//        [HttpGet("info")]
//        [AllowAnonymous]
//        public IActionResult GetApiInfo()
//        {
//            var endpoints = new[]
//            {
//                new { Method = "GET", Path = "/api/Student/profile", Description = "جلب ملف الطالب (يتطلب تسجيل دخول كطالب)", Auth = "Student only" },
//                new { Method = "GET", Path = "/api/Student/test-auth", Description = "اختبار المصادقة والـ Claims", Auth = "Any authenticated user" },
//                new { Method = "GET", Path = "/api/Student/levels/{stageId}", Description = "جلب المستويات حسب المرحلة", Auth = "Any authenticated user" },
//                new { Method = "GET", Path = "/api/Student/test/{userId}", Description = "اختبار جلب الملف (بدون مصادقة)", Auth = "Public" },
//                new { Method = "GET", Path = "/api/Student/info", Description = "معلومات حول API", Auth = "Public" }
//            };

//            return Ok(new ApiResponse<object>(
//                (int)HttpStatusCode.OK,
//                "API الطالب يعمل بنجاح",
//                new
//                {
//                    Endpoints = endpoints,
//                    Version = "1.0",
//                    Status = "Active",
//                    BaseUrl = "/api/Student"
//                }));
//        }
//    }
//}


using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Student;
using Adros.Application.Interfaces.IService;
using Adros.Shared.Constants;
using Adros.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Adros.Apis.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
<<<<<<< HEAD
    //[Authorize(Roles = SystemRoles.Student)]
=======
    [Authorize(Roles = SystemRoles.Student)]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            IStudentService studentService,
            ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        /// <summary>
        /// جلب ملف الطالب الشخصي (للطالب فقط)
        /// </summary>
        [HttpGet("profile")]
        [Authorize(Roles = "Student")]
        [ProducesResponseType(typeof(ApiResponse<StudentProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                _logger.LogInformation("=== بدء جلب ملف الطالب ===");

                // 1. تحقق من أن المستخدم طالب
                if (!User.IsInRole("Student"))
                {
                    return StatusCode((int)HttpStatusCode.Forbidden,
                        new ApiResponse<string>(
                            (int)HttpStatusCode.Forbidden,
                            "غير مصرح - يجب أن تكون طالباً",
                            "User is not a Student"));
                }

                // 2. احصل على UserId من الـ Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new ApiResponse<string>(
                        (int)HttpStatusCode.Unauthorized,
                        "غير مصرح - مستخدم غير معروف",
                        "User ID not found in token"));
                }

                var userId = Guid.Parse(userIdClaim);
                _logger.LogInformation($"جلب ملف الطالب للمستخدم: {userId}");

                // 3. جلب الملف من الـ Service
                var profile = await _studentService.GetStudentProfileAsync(userId);

                return Ok(new ApiResponse<StudentProfileDto>(
                    (int)HttpStatusCode.OK,
                    "تم جلب ملف الطالب بنجاح",
                    profile));
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "الطالب غير موجود");
                return NotFound(new ApiResponse<string>(
                    (int)HttpStatusCode.NotFound,
                    ex.Message,
                    "الطالب غير موجود في النظام"));
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "خطأ في تنسيق UserId");
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "خطأ في معرّف المستخدم",
                    "Invalid User ID format"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب ملف الطالب");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "حدث خطأ في الخادم",
                        ex.Message));
            }
        }

        
        
    }
}


