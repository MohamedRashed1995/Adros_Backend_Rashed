using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Skills;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController(ISkillsService SkillsService, ILogger<SkillsController> logger) : BaseApiController
    {
        private readonly ISkillsService _skillService = SkillsService;
        private readonly ILogger<SkillsController> _logger = logger;

     
        [HttpPost("CreateSkill")]
        [ProducesResponseType(typeof(ApiResponse<SkillDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<SkillDto>> CreateSkillAsync([FromForm] SkillDto skillDto)
        {
            if (!ModelState.IsValid)
            {
                // Collect all validation errors
                var errors = string.Join("; ", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));

                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
            }

            try
            {
                var createdSkill = await _skillService.CreateVariousSkillAsync(skillDto);

                return Ok(
                           new ApiResponse<SkillDto>((int)HttpStatusCode.Created, "Skill Created Successfully", createdSkill)
                );


            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                _logger.LogError(ex, "Error creating Skill.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while creating the Skill.", string.Empty));
            }
        }


        [HttpPut("update/{SkillId}")]
        [ProducesResponseType(typeof(ApiResponse<SkillDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<SkillDto>> UpdateSkillAsync([FromForm] SkillDto skillDto)
        {
            if (!ModelState.IsValid)
            {
                // Collect all validation errors
                var errors = string.Join("; ", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));

                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
            }

            try
            {
                var updatedSkill = await _skillService.UpdateVariousSkillAsync(skillDto);
                if (updatedSkill == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Skill not found.", null));
                }

                return Ok(new ApiResponse<SkillDto>((int)HttpStatusCode.OK, "skill Updated Successfully", updatedSkill));
            }
            catch (Exception ex)
            {
                
                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while updating the Skill.", null));
            }
        }

        [HttpDelete("delete/{SkillId}")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<string>> DeleteSkillAsync(Guid SkillId)
        {
            try
            {
                var isDeleted = await _skillService.DeleteVariousSkillAsync(SkillId);
                if (!isDeleted)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Skill not found.", string.Empty));
                }

                return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Skill deleted successfully.", string.Empty));
            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                _logger.LogError(ex, "Error deleting Skill.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the Skill.", string.Empty));
            }
        }


        [HttpGet("SkillById/{SkillId}")]
        [ProducesResponseType(typeof(ApiResponse<SkillDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<SkillDto>> GetSkillByIdAsync(Guid SkillId)
        {
            try
            {
                var Skill = await _skillService.GetVariousSkillByIdAsync(SkillId);
                if (Skill == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Skill not found.", string.Empty));
                }

                return Ok(new ApiResponse<SkillDto>((int)HttpStatusCode.OK, "Skill Retrieved Successfully", Skill));
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error retrieving Skill.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the Skill.", string.Empty));
            }
        }

        [HttpGet("GetAllSkills")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SkillDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IReadOnlyList<SkillDto>>> GetAllSkills(int? take = null , int? skip=null)
        {
            try
            {
                var skills = await _skillService.GetVariousSkillAsync(take , skip);
                return Ok(skills);
            }
            catch (Exception ex)
            {
                // Log exception if necessary
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred.",
                    Details = ex.Message, // Include exception message for better debugging
                    StackTrace = ex.StackTrace // Optionally include the stack trace
                });
            }
        }

    }
}
