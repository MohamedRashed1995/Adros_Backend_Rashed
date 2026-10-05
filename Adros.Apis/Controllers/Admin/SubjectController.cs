using Adros.Apis.ApiResponse;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Course;
using Adros.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Admin
{
    
    public class SubjectController(ISubjectService subjectService, ILogger<SubjectController> logger) : BaseApiController
    {

        private readonly ISubjectService _subjectService=subjectService;
        private readonly ILogger<SubjectController> _logger = logger;


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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return Ok("Subject Deleted Successfully");
        }
    }
}
