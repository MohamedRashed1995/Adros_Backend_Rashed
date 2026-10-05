using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Teacher;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{
    
    public class TeachersController(ITeacherService teacherService, ILogger<TeachersController> logger) : BaseApiController
    {
        private readonly ITeacherService _teacherService = teacherService;
        private readonly ILogger<TeachersController> _logger = logger;

        /// <summary>
        /// Retrieve all teachers with pagination, filtering, and sorting.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResult<TeacherEntityDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResponse<PaginatedResult<TeacherEntityDto>>>> GetAllTeachers(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool? isActive = null,
            [FromQuery] string? sort = null)
        {
            var result = await _teacherService.GetAllTeachersAsync(page, pageSize, isActive, sort);
            return Ok(new ApiResponse<PaginatedResult<TeacherEntityDto>>((int)HttpStatusCode.OK, "Teachers retrieved successfully.", result));
        }

        /// <summary>
        /// Retrieve a specific teacher by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TeacherEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ApiResponse<TeacherEntityDto>>> GetTeacherById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.NotFound, "Teacher not found.", new TeacherEntityDto()));
            }
            return Ok(teacher);
        }

        /// <summary>
        /// Create a new teacher.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TeacherEntityDto>>> CreateTeacher([FromForm] TeacherCreateDto teacherCreateDto)
        {
            _logger.LogInformation("🎯 === بدء CreateTeacher في Controller ===");
            _logger.LogInformation("📥 البيانات المستلمة:");
            _logger.LogInformation("   Email: {Email}", teacherCreateDto.Email);
            _logger.LogInformation("   FirstName: {firstname}", teacherCreateDto.FirstName);
            _logger.LogInformation("   LastName: {firstname}", teacherCreateDto.FirstName);
            _logger.LogInformation("   Photo: {HasPhoto}", teacherCreateDto.Photo != null ? "نعم" : "لا");

            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    _logger.LogWarning("❌ ModelState غير صالح: {@Errors}", errors);

                    return BadRequest(new ApiResponse<object>(
                        (int)HttpStatusCode.BadRequest,
                        "Validation Errors",
                        new { Errors = errors }
                    ));
                }

                _logger.LogInformation("📞 استدعاء الـ Service...");
                var createdTeacher = await _teacherService.CreateTeacherAsync(teacherCreateDto);

                _logger.LogInformation("✅ النجاح! تم إنشاء المعلم - ID: {TeacherId}", createdTeacher.Id);

                // تحويل الـ Teacher إلى TeacherEntityDto
                var teacherDto = new Teacher
                {
                    Id = createdTeacher.Id,
                    Email = createdTeacher.Email,
                    FirstName = createdTeacher.FirstName,
                    LastName = createdTeacher.LastName,
                    About = createdTeacher.About,
                    CreatedAt = createdTeacher.CreatedAt,
                    UpdatedAt = createdTeacher.UpdatedAt
                };
                //await _teacherService.CreateTeacherAsync(teacherDto);
                return Ok(new ApiResponse<Teacher>(
                    (int)HttpStatusCode.Created,
                    "تم إنشاء المعلم بنجاح",
                    teacherDto
                ));
            }
            catch (ApplicationException appEx)
            {
                _logger.LogWarning(appEx, "⚠️ خطأ في التطبيق");
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "خطأ في البيانات",
                    appEx.Message
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "🔥 خطأ غير متوقع");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<object>(
                        (int)HttpStatusCode.InternalServerError,
                        "حدث خطأ أثناء إنشاء المعلم",
                        new
                        {
                            ErrorMessage = ex.Message,
                            StackTrace = ex.StackTrace,
                            InnerException = ex.InnerException?.Message
                        }
                    ));
            }
        }

        /// <summary>
        /// Update teacher details.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TeacherEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ApiResponse<TeacherEntityDto>>> UpdateTeacher(Guid id, [FromForm] TeacherUpdateDto teacherUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.BadRequest, "Invalid input.", new TeacherEntityDto()));
            }

            var updatedTeacher = await _teacherService.UpdateTeacherAsync(id, teacherUpdateDto);
            if (updatedTeacher == null)
            {
                return NotFound(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.NotFound, "Teacher not found.", new TeacherEntityDto()));
            }
            return Ok(updatedTeacher);
        }

        /// <summary>
        /// Delete a teacher.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ApiResponse<string>>> DeleteTeacher(Guid id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Teacher not found.", string.Empty));
            }
            return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Teacher deleted successfully.", "Teacher has been deleted."));
        }

        /// <summary>
        /// Activate or deactivate a teacher's account.
        /// </summary>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ApiResponse<string>>> UpdateTeacherStatus(Guid id, [FromBody] TeacherStatusUpdateDto statusUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Invalid input.", string.Empty));
            }

            var result = await _teacherService.UpdateTeacherStatusAsync(id, statusUpdateDto.IsActive);
            if (!result)
            {
                return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Teacher not found.", string.Empty));
            }
            return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Teacher status updated successfully.", "Teacher account status has been updated."));
        }

        /// <summary>
        /// Change a teacher's password.
        /// </summary>
        [HttpPost("{id}/change-password")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ApiResponse<string>>> ChangeTeacherPassword(Guid id, [FromBody] TeacherChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Invalid input.", string.Empty));
            }

            var result = await _teacherService.ChangeTeacherPasswordAsync(id, changePasswordDto.NewPassword);
            if (!result)
            {
                return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Teacher not found.", string.Empty));
            }
            return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Password changed successfully.", "Teacher's password has been updated."));
        }
    }
}

