using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Pagination;
using Adros.Application.DTOs.Student;
using Adros.Application.Interfaces.IService;
using Adros.Core.Specifications.QueryParams;
using Adros.Shared.Constants;
using Adros.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace Adros.Apis.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentsController(IStudentService studentService,ILogger<StudentsController> logger) : BaseApiController
    {
        private readonly IStudentService _studentService = studentService;
        private readonly ILogger<StudentsController> _logger = logger;

        /// <summary>
        /// Get paginated list of students with filtering and sorting
        /// </summary>
        /// <param name="parameters">Query parameters for filtering, sorting and pagination</param>
        /// <response code="200">Returns paginated list of students</response>
        /// <response code="400">Invalid query parameters</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<StudentListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<StudentListDto>>> GetAllStudents()
        {
            try
            {
                var result = await _studentService.GetAllStudentsAsync();
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, ApiMessages.Students.InvalidParameters);
                return Problem(
                    title: ApiMessages.Students.InvalidParameters,
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
            catch (StudentServiceException ex)
            {
                _logger.LogError(ex, ApiMessages.Students.StudentError);
                return Problem(
                    title: ApiMessages.Students.StudentError,
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }




        /// <summary>
        /// Retrieves a single student by ID
        /// </summary>
        /// <param name="id">Unique identifier of the student</param>
        /// <response code="200">Student retrieved successfully</response>
        /// <response code="404">Student not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentEntityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentEntityDto>> GetStudentById([FromRoute] Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);

                return Ok(new ApiResponse<StudentEntityDto>(
                    (int)HttpStatusCode.OK,
                    ApiMessages.Students.StudentRetrieved,
                    student
                ));
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, ApiMessages.Students.StudentNotFound);
                return Problem(
                    title: ApiMessages.Students.StudentNotFound,
                    detail: ex.Message,
                    statusCode: StatusCodes.Status404NotFound
                );
            }
            catch (StudentServiceException ex)
            {
                _logger.LogError(ex, ApiMessages.Students.StudentError);
                return Problem(
                    title: ApiMessages.Students.StudentError,
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }



        /// <summary>
        /// Changes the activation status of a student
        /// </summary>
        /// <param name="id">Student identifier</param>
        /// <param name="isActive">True to activate, false to deactivate</param>
        /// <response code="200">Returns a success message</response>
        /// <response code="404">Student not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("{id}/activation")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStudentActivation(
            [FromRoute] Guid id,
            [FromBody] bool isActive
        )
        {
            try
            {
                await _studentService.ChangeStudentActivationAsync(id, isActive);
                return Ok(new ApiResponse<string>(
                    (int)HttpStatusCode.OK,
                    "Student activation updated.",
                    string.Empty
                ));
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, ApiMessages.Students.StudentNotFound);
                return Problem(
                    title: ApiMessages.Students.StudentNotFound,
                    detail: ex.Message,
                    statusCode: StatusCodes.Status404NotFound
                );
            }
            catch (StudentServiceException ex)
            {
                _logger.LogError(ex, ApiMessages.Students.StudentError);
                return Problem(
                    title: ApiMessages.Students.StudentError,
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }





    }
}
