
using Adros.Apis.ApiResponse;
using Adros.Application.DTOs;
using Adros.Application.DTOs.Auth;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Users;
using Adros.Core.Specifications;
using Adros.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Security.Claims;

namespace Adros.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            ILogger<AuthController> logger,
            IUnitOfWork unitOfWork,
            
            IConfiguration configuration,
            IEmailService? emailService = null)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailService = emailService ;
            _configuration = configuration;
        }

        // ========== التسجيل ==========
        //[HttpPost("Register")]
        //public async Task<ActionResult> Register([FromBody] RegisterDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new ApiResponse<ModelStateDictionary>((int)HttpStatusCode.BadRequest, "Invalid data!", ModelState));
        //    }

        //    var userExists = await _userManager.FindByNameAsync(model.Email);
        //    if (userExists != null)
        //    {
        //        return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "User already exists!", string.Empty));
        //    }

        //    var user = new ApplicationUser
        //    {
        //        Id = new Guid(),
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        UserName = model.Email,
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    };

        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(result.Errors);
        //    }

        //    var roleResult = await _userManager.AddToRoleAsync(user, "Student");
        //    if (!roleResult.Succeeded)
        //    {
        //        _logger.LogWarning("Failed to assign Student role to user.");
        //        throw new ApplicationException("Failed to assign role.");
        //    }

        //    var student = new Student
        //    {
        //        Id = user.Id,
        //        ApplicationUser = user,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        //LevelId = user.Student.LevelId,
        //        Government = "",
        //        City = "",
        //        BirthDate = null,
        //        CreatedAt = DateTime.UtcNow,
        //        CreatedBy = user.Id,
        //        ApplicationUserId = user.Id,
        //    };

        //    await _unitOfWork.Repository<Student>().AddAsync(student);
        //    await _unitOfWork.CompleteAsync();

        //    if (result.Succeeded)
        //    {
        //        return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "User created successfully!", string.Empty));
        //    }

        //    return BadRequest(new ApiResponse<IEnumerable<IdentityError>>((int)HttpStatusCode.BadRequest, "Registration failed!", result.Errors));
        //}
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<ModelStateDictionary>((int)HttpStatusCode.BadRequest, "Invalid data!", ModelState));
            }

            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "User already exists!", string.Empty));
            }

            var user = new ApplicationUser
            {
<<<<<<< HEAD
                
=======
                Id = Guid.NewGuid(),
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,   
                LastName = model.LastName,  
                
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,          
                PhoneNumberConfirmed = true,    
                IsActive = true ,
               
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!roleResult.Succeeded)
            {
                _logger.LogWarning("Failed to assign Student role to user.");
                throw new ApplicationException("Failed to assign role.");
            }
            try
            {
                var student = new Student
                {
<<<<<<< HEAD
                    Id =Guid.NewGuid(),
=======
                    Id = user.Id,
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                    ApplicationUser = user,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = user.Email,
                    Government = "",
                    City = "",
                    BirthDate = null,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = user.Id,
                    ApplicationUserId = user.Id,
<<<<<<< HEAD
                    IsSubscriped = false,
                    LevelId = model.LevelId,
                    WatchLater = new List<WatchLater>(),
=======
                    SubscriptionStatus = "inactive",
                    LevelId = model.LevelId,
                    VideoDownloads = new List<VideoDownload>(),
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                    VideoViews = new List<VideoView>()
                };
                await _unitOfWork.Repository<Student>().AddAsync(student);
                await _unitOfWork.CompleteAsync();

            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw;
            }


            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "User created successfully!", string.Empty));
            }

            return BadRequest(new ApiResponse<IEnumerable<IdentityError>>((int)HttpStatusCode.BadRequest, "Registration failed!", result.Errors));
        }

        // ========== الدخول ==========
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<ModelStateDictionary>(
                    (int)HttpStatusCode.BadRequest, "Invalid data!", ModelState));
            }

            ApplicationUser? user = null;

            if (!string.IsNullOrEmpty(model.Email))
            {
                user = await _userManager.FindByNameAsync(model.Email)
                    ?? await _userManager.FindByEmailAsync(model.Email);
            }

            if (user == null)
            {
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest, "User does not exist!", string.Empty));
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest, "Invalid credentials!", string.Empty));
            }

            // ✅ هنا بقى نجيب Student
            var student = await _unitOfWork.Repository<Student>()
                .GetEntityWithSpec(new StudentByUserIdSpecification(user.Id));

            var token = await _tokenService.CreateTokenAsync(user, _userManager);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new ApiResponse<LoginResponse>(
                (int)HttpStatusCode.OK,
                "Login successful!",
                new LoginResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    LevelId = student?.LevelId, // ✅ آمن
                    Token = token,
                    ValidTo = DateTime.UtcNow.AddDays(30).ToString(),
                    Role = roles.FirstOrDefault()
                }));
        }


        // ========== إعادة تعيين كلمة المرور (الطريقة المباشرة) ==========
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            try
            {
                _logger.LogInformation($"Password reset requested for: {request.Email}");

                // 1. التحقق من البيانات
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<ModelStateDictionary>(
                        (int)HttpStatusCode.BadRequest,
                        "بيانات غير صالحة!",
                        ModelState
                    ));
                }

                //if (!ModelState.IsValid)


                // 2. التحقق من تطابق كلمات المرور
                if (request.NewPassword != request.ConfirmPassword)
                {
                    return BadRequest(new ApiResponse<string>(
                        (int)HttpStatusCode.BadRequest,
                        "كلمتا المرور غير متطابقتين.",
                        string.Empty
                    ));
                }

                // 3. البحث عن المستخدم
                var user = await _userManager.FindByEmailAsync(request.Email);

                // 4. نفس الرد للأمان (سواء وجد المستخدم أم لا)
                if (user == null)
                {
                    _logger.LogWarning($"Password reset attempt for non-existent email: {request.Email}");

                    // انتظار بسيط لمنع هجمات التوقيت
                    await Task.Delay(1500);

                    return Ok(new ApiResponse<string>(
                        (int)HttpStatusCode.OK,
                        "  الايميل غير مسجل على منصة ادرس",
                        string.Empty
                    ));
                }

                _logger.LogInformation($"User found: {user.Email}, resetting password...");

                // 5. توليد Token وإعادة تعيين كلمة المرور
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

                if (!resetResult.Succeeded)
                {
                    var errors = resetResult.Errors.Select(e => e.Description).ToList();
                    _logger.LogError($"Password reset failed for {user.Email}. Errors: {string.Join(", ", errors)}");

                    // رسالة خطأ واضحة
                    var errorMessage = "فشل إعادة تعيين كلمة المرور. ";

                    if (errors.Any(e => e.Contains("Password", StringComparison.OrdinalIgnoreCase)))
                    {
                        errorMessage += "كلمة المرور يجب أن تحتوي على أحرف كبيرة وصغيرة وأرقام ورموز.";
                    }
                    else if (errors.Any(e => e.Contains("token", StringComparison.OrdinalIgnoreCase)))
                    {
                        errorMessage += "الرمز غير صالح. يرجى طلب إعادة تعيين جديدة.";
                    }

                    return BadRequest(new ApiResponse<string>(
                        (int)HttpStatusCode.BadRequest,
                        errorMessage,
                        string.Empty
                    ));
                }

                // 6. إرسال إيميل التنبيه
                

                // 7. تحديث Security Stamp (تسجيل الخروج من جميع الأجهزة)
                await _userManager.UpdateSecurityStampAsync(user);

                // 8. تسجيل النجاح
                _logger.LogInformation($"✅ Password successfully reset for user: {user.Email}");

                return Ok(new ApiResponse<string>(
                    (int)HttpStatusCode.OK,
                    "✅ تم تغيير كلمة المرور بنجاح! تم إرسال إيميل تأكيد إلى بريدك الإلكتروني.",
                    string.Empty
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in password reset for email: {request.Email}");

                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "حدث خطأ غير متوقع. يرجى المحاولة مرة أخرى.",
                        string.Empty
                    )
                );
            }
        }

        

        // ========== دالة للتحقق من صحة البريد (اختياري) ==========
        [HttpPost("CheckEmail")]
        [AllowAnonymous]
        public async Task<ActionResult> CheckEmail([FromBody] CheckEmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "البريد الإلكتروني مطلوب",
                    string.Empty
                ));
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            
            return Ok(new ApiResponse<bool>(
                (int)HttpStatusCode.OK,
                "تم التحقق من البريد الإلكتروني",
                user != null
            ));
        }
        [HttpGet("validate-token")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            }).ToList();

            return Ok(new
            {
                isValid = true,
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                email = User.FindFirstValue(ClaimTypes.Email),
                roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value),
                claims = claims,
                issuedAt = DateTime.UtcNow
            });
        }
<<<<<<< HEAD
        
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpPost("forget-password")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgetPassword([FromBody] ForgotPasswordDto request)
        {
            try
            {
                _logger.LogInformation($"Forget password request for: {request.Email}");

                // 1. التحقق من البيانات
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<ModelStateDictionary>(
                        (int)HttpStatusCode.BadRequest,
                        "بيانات غير صالحة!",
                        ModelState
                    ));
                }

                // 2. البحث عن المستخدم
                var user = await _userManager.FindByEmailAsync(request.Email);

                // 3. للأمان: نفس الرسالة سواء وجد المستخدم أم لا
                if (user == null)
                {
                    _logger.LogWarning($"Forget password attempt for non-existent email: {request.Email}");

                    // تأخير بسيط لمنع هجمات التوقيت
                    await Task.Delay(1500);

                    return Ok(new ApiResponse<string>(
                        (int)HttpStatusCode.OK,
                        "الايميل غير مسجل على منصة ادرس",
                        string.Empty
                    ));
                }

                _logger.LogInformation($"User found: {user.Email}, generating reset token...");

                // 4. توليد Token لإعادة التعيين
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                // 5. إنشاء رابط إعادة التعيين
                var resetLink = $"https://yourfrontend.com/reset-password?token={Uri.EscapeDataString(resetToken)}&email={Uri.EscapeDataString(user.Email)}";

                // 6. هنا يمكنك إرسال الإيميل (لاحقاً)
                _logger.LogInformation($"Reset link for {user.Email}: {resetLink}");

                // 7. للاختبار: يمكنك إرجاع الرابط (فقط في البيئة التطويرية)
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    return Ok(new ApiResponse<string>(
                        (int)HttpStatusCode.OK,
                        "تم إنشاء رابط إعادة التعيين. (للتطوير فقط)",
                        resetLink
                    ));
                }

                // 8. في البيئة الإنتاجية
                return Ok(new ApiResponse<string>(
                    (int)HttpStatusCode.OK,
                    "إذا كان البريد الإلكتروني مسجلاً لدينا، ستتلقى رابط إعادة التعيين في خلال دقائق.",
                    string.Empty
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in forget password for email: {request.Email}");

                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "حدث خطأ غير متوقع. يرجى المحاولة مرة أخرى.",
                        string.Empty
                    )
                );
            }
        }
    }







    // ========== DTOs إضافية داخل الكونترولر ==========
    public class CheckEmailRequest
    {
        public string Email { get; set; } = default!;
    }
}