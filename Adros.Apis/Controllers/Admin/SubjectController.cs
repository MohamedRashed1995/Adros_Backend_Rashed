using Adros.Apis.ApiResponse;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Course;
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = SystemRoles.Teacher)]
    public class SubjectController(ISubjectService subjectService, ILogger<SubjectController> logger) : BaseApiController
    {

        private readonly ISubjectService _subjectService=subjectService;
        private readonly ILogger<SubjectController> _logger = logger;

        //[Authorize(Roles = SystemRoles.Student)]
        //[AllowAnonymous]
        [HttpGet("GetSubjects")]
        public async Task<ActionResult<Pagination<SubjectDto>>> GetSubjects([FromQuery] SubjectSpecParams subjectSpecParams)
        {
            try
            {
                var result = await _subjectService.GetSubjectsAsync(subjectSpecParams);
                return Ok(result);
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, "An error occurred while fetching the subjects."));
            }
        }
<<<<<<< HEAD
       
=======
        //[Authorize(Roles =SystemRoles.)]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpGet("GetSubjectById/{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubjectById(Guid id)
        {
            try
            {
                return Ok(await _subjectService.GetSubjectByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(404, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, ex.Message));
            }
        }
        //[HttpGet("GetSubjectByLevelId/{id}")]
        //public async Task<ActionResult<SubjectDto>> GetSubjectByLevelId(Guid id)
        //{
        //    try
        //    {
        //        return Ok(await _subjectService.GetSubjectByLevelIdAsync(id));
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new ApiResponse<string>(404, ex.Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApiResponse<string>(500, ex.Message));
        //    }
        //}
<<<<<<< HEAD
        //[Authorize(Roles = SystemRoles.Teacher)]
=======
        [Authorize(Roles = SystemRoles.Teacher)]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpPost("CreateSubject")]
        public async Task<ActionResult<SubjectDto>> CreateSubject([FromBody] SubjectInputDto dto)
        {
            //return CreatedAtAction(nameof(GetSubjectById), new { id = dto.LevelId }, await _subjectService.CreateSubjectAsync(dto));
            try {
                return Ok(await _subjectService.CreateSubjectAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, ex.Message));
            }
        }
        [Authorize(Roles = SystemRoles.Teacher)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] SubjectInputDto dto)
        {
            try
            {
                await _subjectService.UpdateSubjectAsync(id, dto);
                return Ok("Subject Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(500, ex.Message));
            }
        }
        //[Authorize(Roles = SystemRoles.Teacher)]
        [HttpGet("GetSubjectsByLevel/{levelId}")]
        public async Task<ActionResult<IReadOnlyList<SubjectDto>>> GetSubjectsByLevel(Guid levelId)
        {
            try
            {
                var subjects = await _subjectService.GetSubjectsByLevelIdAsync(levelId);
                return Ok(new ApiResponse<IReadOnlyList<SubjectDto>>(
                            200,
                            "Subjects retrieved successfully for the given level.",
                            subjects));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message));
            }
        }



<<<<<<< HEAD
        //[Authorize(Roles =SystemRoles.Teacher)]
=======
        [Authorize(Roles =SystemRoles.Teacher)]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return Ok("Subject Deleted Successfully");
        }
    }
}
