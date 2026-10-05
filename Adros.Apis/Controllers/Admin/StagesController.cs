using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Adros.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.ServiceModel.Channels;

namespace Adros.Apis.Controllers.Admin
{
    //[Authorize(Roles = SystemRoles.Master)]
    public class StagesController(IStageService stageService, ILogger<StagesController> logger) : BaseApiController
    {
        private readonly IStageService _stageService = stageService;
        private readonly ILogger<StagesController> _logger = logger;
        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<StageEntityDto>>> GetAllStagesAsync()
        {
            try
            {
                var stages = await _stageService.GetClientStagesAsync();
                //var stages = await _stageService.GetClientStagesAsync();
                return Ok(new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Stage List",
                            data = stages
                        }
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all stages.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<StageEntityDto>> Create([FromForm] StageCreateDto stageCreateDto)
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
                var createdStage = await _stageService.CreateStageAsync(stageCreateDto);
                return Ok(
                           new ApiResponse<StageEntityDto>((int)HttpStatusCode.Created, "Stage Created Successfully", createdStage)
                );
            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                _logger.LogError(ex, "Error creating stage.");
                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while creating the stage.", string.Empty));
            }

            }

        [HttpPut("{stageId}")]
        public async Task<ActionResult<ClientStageDto>> Update(Guid stageId, [FromForm] StageUpdateDto stageUpdateDto)
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
                var updatedStage = await _stageService.UpdateStageAsync(stageId, stageUpdateDto);
                if (updatedStage == null) return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "stage not found.", string.Empty));
                return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK,"Stage Updated Successfully.",updatedStage));
            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                _logger.LogError(ex, "Error updating stage.");
                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while updating the stage.", string.Empty));
            }
                
        }

        /// <summary>
        /// Retrieves a specific stage by its ID.
        /// </summary>
        /// <param name="stageId">The ID of the stage to retrieve.</param>
        /// <returns>The requested stage details.</returns>
        [HttpGet("{stageId}")]
        [ProducesResponseType(typeof(ApiResponse<StageEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StageEntityDto>> GetById(Guid stageId)
        {
            try
            {
                var stage = await _stageService.GetStageByIdAsync(stageId);
                if (stage == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
                }

                return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK, "Stage Retrieved Successfully", stage));
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error retrieving Stage.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the Stage.", string.Empty));
            }
        }

        /// <summary>
        /// Deletes an existing stage.
        /// </summary>
        /// <param name="stageId">The ID of the stage to delete.</param>
        /// <returns>Status of the deletion.</returns>
        [HttpDelete("{stageId}")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<string>> Delete(Guid stageId)
        {
            try
            {
                var isDeleted = await _stageService.DeleteStageAsync(stageId);
                if (!isDeleted)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
                }
                await _stageService.DeleteStageAsync(stageId);
                return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Stage deleted successfully.", string.Empty));
            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                _logger.LogError(ex, "Error deleting Stage.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the stage.", string.Empty));
            }
        }


       


    }
}
