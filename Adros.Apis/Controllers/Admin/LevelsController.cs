using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Level;
using Adros.Application.Interfaces.IService;
using Adros.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{
    [Authorize(Roles = SystemRoles.Master)]
    public class LevelsController(ILevelService levelService, ILogger<LevelsController> logger) : BaseApiController
    {
        private readonly ILevelService _levelService = levelService;
        private readonly ILogger<LevelsController> _logger = logger;

        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LevelEntityDto>> CreateAsync([FromBody] LevelCreateDto levelCreateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
            }

            try
            {
                var createdLevel = await _levelService.CreateLevelAsync(levelCreateDto);
                return Ok(new ApiResponse<LevelEntityDto>((int)HttpStatusCode.Created, "Level Created", createdLevel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating level.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error creating level", string.Empty));
            }
        }

        [HttpPut("update/{levelId}")]
        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<LevelEntityDto>> UpdateAsync(Guid levelId, [FromBody] LevelUpdateDto levelUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
            }

            try
            {
                var updatedLevel = await _levelService.UpdateLevelAsync(levelId, levelUpdateDto);
                if (updatedLevel == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Level not found", string.Empty));
                }

                return Ok(new ApiResponse<LevelEntityDto>((int)HttpStatusCode.OK, "Level Updated", updatedLevel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating level.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error updating level", string.Empty));
            }
        }

        [HttpGet("{levelId}")]
        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<LevelEntityDto>> GetByIdAsync(Guid levelId)
        {
            try
            {
                var level = await _levelService.GetLevelByIdAsync(levelId);
                if (level == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Level not found", string.Empty));
                }

                return Ok(new ApiResponse<LevelEntityDto>((int)HttpStatusCode.OK, "Level Retrieved", level));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving level.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error retrieving level", string.Empty));
            }
        }

        [HttpGet("by-stage/{stageId}")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<LevelEntityDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<LevelEntityDto>>> GetByStageIdAsync(Guid stageId)
        {
            try
            {
                var levels = await _levelService.GetLevelsByStageIdForAdminAsync(stageId);
                return Ok(new ApiResponse<IReadOnlyList<LevelEntityDto>>((int)HttpStatusCode.OK, "Levels Retrieved", levels));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving levels by stage ID.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error retrieving levels by stage", string.Empty));
            }
        }

        [HttpDelete("delete/{levelId}")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<string>> DeleteAsync(Guid levelId)
        {
            try
            {
                var deleted = await _levelService.DeleteLevelAsync(levelId);
                if (!deleted)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Level not found", string.Empty));
                }

                return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Level deleted", string.Empty));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting level.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error deleting level", string.Empty));
            }
        }
    }
}
