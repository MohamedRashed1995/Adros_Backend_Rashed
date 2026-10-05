using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Level;
using Adros.Application.DTOs.Student;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Users;
using Adros.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Adros.Apis.Controllers.Client
{

    public class StudentController(IStudentService studentService,ICurrentUserService currentUserService,ISharedUserService sharedUserService,ILevelService levelService, ILogger<StudentController> logger) : BaseApiController
    {
         
        private readonly IStudentService _studentService = studentService;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ISharedUserService _sharedUserService = sharedUserService;
        private readonly ILevelService _levelService = levelService;
        private readonly ILogger<StudentController> _logger = logger;


        /// <summary>
        /// Retrieves a student's profile with detailed data
        /// </summary>
        /// <param name="id">Student's unique identifier</param>
        /// <response code="200">Student profile retrieved</response>
        /// <response code="404">Student not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("profile")]
        [ProducesResponseType(typeof(StudentProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetStudentProfile()
        {
            // 1️⃣ نجيب UserId من التوكن
            try 
            {
                // 2️⃣ نجيب الـ User + Student
                //var userid = _currentUserService.UserId;
                //var profile = _studentService.GetStudentProfileAsync(userid);

                //return Ok(profile);
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("Invalid token");

                var userId = Guid.Parse(userIdClaim);

                // 2️⃣ نجيب StudentProfileDto من Service
                var profile = await _studentService.GetStudentProfileAsync(userId);

                return Ok(profile);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
                }
        }


        //public async Task<ActionResult<Student>> GetStudentProfile(Guid userId)
        //{
        //    userId = _currentUserService.UserId; 
        //    try
        //    {
        //        //var userId = _currentUserService.UserId;
        //        var profile = await _studentService.GetStudentProfileAsync(userId);
        //        return Ok(profile);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        _logger.LogWarning(ex, "Student not found");
        //        return Problem(
        //            title: "Student not found",
        //            detail: ex.Message,
        //            statusCode: StatusCodes.Status404NotFound
        //        );
        //    }
        //    catch (StudentServiceException ex)
        //    {
        //        _logger.LogError(ex, "Error retrieving student profile");
        //        return Problem(
        //            title: "Error retrieving student profile",
        //            detail: ex.Message,
        //            statusCode: StatusCodes.Status500InternalServerError
        //        );
        //    }
        //}



        [HttpGet("levels/{stageId}")]
        [ProducesResponseType(typeof(ApiResponse<List<ClientLevelDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ClientLevelDto>>> GetByStageIdAsync(Guid stageId)
        {
            try
            {
                var levels = await _levelService.GetLevelsForClientByStageIdAsync(stageId);
                return Ok(new ApiResponse<List<ClientLevelDto>>((int)HttpStatusCode.OK, "Levels Retrieved", levels.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving levels by stage ID.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error retrieving levels by stage", string.Empty));
            }
        }
    }
}
