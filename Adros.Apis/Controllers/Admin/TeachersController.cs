using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Teacher;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // تأكد إن المستخدم Admin
    public class TeachersController : BaseApiController
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeachersController> _logger;

        public TeachersController(ITeacherService teacherService, ILogger<TeachersController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginatedResult<TeacherEntityDto>>>> GetAllTeachers(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool? isActive = null,
            [FromQuery] string? sort = null)
        {
            var result = await _teacherService.GetAllTeachersAsync(page, pageSize, isActive, sort);
            return Ok(new ApiResponse<PaginatedResult<TeacherEntityDto>>((int)HttpStatusCode.OK, "Teachers retrieved successfully.", result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TeacherEntityDto>>> GetTeacherById(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.NotFound, "Teacher not found.", new TeacherEntityDto()));

            var teacherDto = new TeacherEntityDto
            {
                TeacherID = teacher.Id,
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                About = teacher.About,
<<<<<<< HEAD
                StageId = teacher.StageId,
                ProfilePictureUrl = "https://adros-mrashed.runasp.net/" + "Uploads/Images/teachers/" + teacher.ProfilePictureUrl,
                ApplicationUserId = teacher.ApplicationUserId,
                IsActive = teacher.IsActive,
                LessonCount = teacher.Lessons.Count,
                CreatedAt = teacher.CreatedAt,
                UpdatedAt = teacher.UpdatedAt,
                CreatedBy = teacher.CreatedBy,
                PhoneNumber = teacher.phoneNumber,
=======
                ApplicationUserId = teacher.ApplicationUserId,
                IsActive = teacher.ApplicationUser.IsActive,
                LessonCount = teacher.Lessons.Count,
                CreatedAt = teacher.CreatedAt,
                UpdatedAt = teacher.UpdatedAt,
                CreatedBy = teacher.CreatedBy
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            };

            return Ok(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.OK, "Teacher retrieved successfully.", teacherDto));
        }

<<<<<<< HEAD
        [HttpGet("by-stage/{stageId}")]
        public async Task<ActionResult<ApiResponse<object>>> GetTeachersByStage(Guid stageId)
        {
            var (teachers, teacherCount) = await _teacherService.GetTeachersByStageAsync(stageId);

            return Ok(new ApiResponse<object>(
                200,
                "Teachers retrieved successfully",
                new
                {
                    TeacherCount = teacherCount,
                    Teachers = teachers
                }
            ));
        }



=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TeacherEntityDto>>> CreateTeacher([FromForm] TeacherCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<object>((int)HttpStatusCode.BadRequest, "Validation errors", new { Errors = errors }));
            }

            var teacher = await _teacherService.CreateTeacherAsync(dto);

            var teacherDto = new TeacherEntityDto
            {
                TeacherID = teacher.Id,
                Email = teacher.Email,
                FirstName = teacher.FirstName,
<<<<<<< HEAD
                ProfilePictureUrl = teacher.ProfilePictureUrl,
                LastName = teacher.LastName,
                About = teacher.About,
                ApplicationUserId = teacher.ApplicationUserId,
                IsActive = teacher.IsActive,
                LessonCount = teacher.Lessons.Count,
                CreatedAt = teacher.CreatedAt,
                UpdatedAt = teacher.UpdatedAt,
                CreatedBy = teacher.CreatedBy,
                StageId = teacher.StageId,
                PhoneNumber = teacher.phoneNumber,
=======
                LastName = teacher.LastName,
                About = teacher.About,
                ApplicationUserId = teacher.ApplicationUserId,
                IsActive = teacher.ApplicationUser.IsActive,
                LessonCount = teacher.Lessons.Count,
                CreatedAt = teacher.CreatedAt,
                UpdatedAt = teacher.UpdatedAt,
                CreatedBy = teacher.CreatedBy
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            };

            return CreatedAtAction(nameof(GetTeacherById), new { id = teacherDto.TeacherID }, new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.Created, "Teacher created successfully", teacherDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TeacherEntityDto>>> UpdateTeacher(Guid id, [FromForm] TeacherUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.BadRequest, "Invalid input.", new TeacherEntityDto()));

            var updatedTeacher = await _teacherService.UpdateTeacherAsync(id, dto);
            if (updatedTeacher == null)
                return NotFound(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.NotFound, "Teacher not found.", new TeacherEntityDto()));

            var teacherDto = new TeacherEntityDto
            {
                TeacherID = updatedTeacher.Id,
                Email = updatedTeacher.Email,
<<<<<<< HEAD
                ProfilePictureUrl = updatedTeacher.ProfilePictureUrl,
                FirstName = updatedTeacher.FirstName,
                LastName = updatedTeacher.LastName,
                About = updatedTeacher.About,
                IsActive = updatedTeacher.IsActive,
                ApplicationUserId = updatedTeacher.ApplicationUserId,
                LessonCount = updatedTeacher.Lessons.Count,
                CreatedAt = updatedTeacher.CreatedAt,
                UpdatedAt = updatedTeacher.UpdatedAt,
                CreatedBy = updatedTeacher.CreatedBy,
                StageId = updatedTeacher.StageId,
                PhoneNumber = updatedTeacher.phoneNumber,   
=======
                FirstName = updatedTeacher.FirstName,
                LastName = updatedTeacher.LastName,
                About = updatedTeacher.About,
                ApplicationUserId = updatedTeacher.ApplicationUserId,
                IsActive = updatedTeacher.ApplicationUser.IsActive,
                LessonCount = updatedTeacher.Lessons.Count,
                CreatedAt = updatedTeacher.CreatedAt,
                UpdatedAt = updatedTeacher.UpdatedAt,
                CreatedBy = updatedTeacher.CreatedBy
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            };

            return Ok(new ApiResponse<TeacherEntityDto>((int)HttpStatusCode.OK, "Teacher updated successfully", teacherDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteTeacher(Guid id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            if (!result)
                return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Teacher not found", string.Empty));

            return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Teacher deleted successfully", "Teacher has been deleted."));
        }
<<<<<<< HEAD



        [HttpPut("{teacherid}/change-password")]
        public async Task<IActionResult> ChangePassword(Guid id,[FromBody] TeacherChangePasswordDto dto)
        {
            var message = await _teacherService.ChangePasswordAsync(id, dto);
            return Ok(message);
        }



=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
