//using Adros.Apis.ApiResponse;
//using Adros.Application.DTOs.Level;
//using Adros.Application.DTOs.Stage;
//using Adros.Application.Interfaces.IService;
//using Adros.Application.Services.HomeService;
//using Adros.Core.Entities.Course;
//using Adros.Shared.Constants;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace Adros.Apis.Controllers.Admin
//{
//    //[Authorize(Roles = SystemRoles.Admin)]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class LevelsController(ILevelService levelService, ILogger<LevelsController> logger) : BaseApiController
//    {
//        private readonly ILevelService _levelService = levelService;
//        private readonly ILogger<LevelsController> _logger = logger;

//        [HttpPost("create")]
//        [Consumes("multipart/form-data")]
//        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.Created)]
//        public async Task<ActionResult<LevelEntityDto>> CreateAsync(
//    [FromForm] LevelCreateDto levelCreateDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                var errors = string.Join("; ",
//                    ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

//                return BadRequest(new ApiResponse<string>(
//                    (int)HttpStatusCode.BadRequest,
//                    "Validation Errors",
//                    errors));
//            }

//            var createdLevel = await _levelService.CreateLevelAsync(levelCreateDto);

//            return Ok(new ApiResponse<LevelEntityDto>(
//                (int)HttpStatusCode.Created,
//                "Level Created",
//                createdLevel));
//        }

//        [HttpGet("all")]
//        public async Task<ActionResult<ApiResponse<IReadOnlyList<LevelEntityDto>>>> GetLevelsAsync()
//        {
//            try
//            {
//                var levels = await _levelService.GetLevelsAsync();
//                return Ok(new ApiResponse<IReadOnlyList<LevelEntityDto>>(
//                    200,
//                    "Level list retrieved successfully.",
//                    levels
//                ));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while fetching all stages.");
//                return Ok(new ApiResponse<string>(
//                    200,
//                    "Failed to fetch stages",
//                    ex.Message
//                ));
//            }
//        }
//        [HttpPut("update/{levelId}")]
//        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.OK)]
//        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
//        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
//        public async Task<ActionResult<LevelEntityDto>> UpdateAsync(Guid levelId, [FromBody] LevelUpdateDto levelUpdateDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                var errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
//                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
//            }

//            try
//            {
//                var updatedLevel = await _levelService.UpdateLevelAsync(levelId, levelUpdateDto);
//                if (updatedLevel == null)
//                {
//                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Level not found", string.Empty));
//                }

//                return Ok(new ApiResponse<LevelEntityDto>((int)HttpStatusCode.OK, "Level Updated", updatedLevel));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error updating level.");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error updating level", string.Empty));
//            }
//        }

//        [HttpGet("{levelId}")]
//        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.OK)]
//        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
//        public async Task<ActionResult<LevelEntityDto>> GetByIdAsync(Guid levelId)
//        {
//            try
//            {
//                var level = await _levelService.GetLevelByIdAsync(levelId);
//                if (level == null)
//                {
//                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Level not found", string.Empty));
//                }

//                return Ok(new ApiResponse<LevelEntityDto>((int)HttpStatusCode.OK, "Level Retrieved", level));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error retrieving level.");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error retrieving level", string.Empty));
//            }
//        }

//        [HttpGet("by-stage/{stageId}")]
//        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<LevelEntityDto>>), (int)HttpStatusCode.OK)]
//        public async Task<ActionResult<IReadOnlyList<LevelEntityDto>>> GetByStageIdAsync(Guid stageId)
//        {
//            try
//            {
//                var levels = await _levelService.GetLevelsByStageIdForAdminAsync(stageId);

//                return Ok(new ApiResponse<IReadOnlyList<LevelEntityDto>>((int)HttpStatusCode.OK, "Levels Retrieved", levels));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error retrieving levels by stage ID.");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//             new ApiResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace));
//            }
//        }

//        [HttpDelete("delete/{levelId}")]
//        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
//        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
//        public async Task<ActionResult<string>> DeleteAsync(Guid levelId)
//        {
//            try
//            {
//                var deleted = await _levelService.DeleteLevelAsync(levelId);
//                if (!deleted)
//                {
//                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Level not found", string.Empty));
//                }

//                return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Level deleted", string.Empty));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error deleting level.");
//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "Error deleting level", string.Empty));
//            }
//        }
//    }
//}



using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Level;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{
    //[Authorize(Roles = SystemRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class LevelsController : BaseApiController
    {
        private readonly ILevelService _levelService;
        private readonly ILogger<LevelsController> _logger;

        public LevelsController(
            ILevelService levelService,
            ILogger<LevelsController> logger)
        {
            _levelService = levelService;
            _logger = logger;
        }

        // ================= CREATE =================
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromForm] LevelCreateDto levelCreateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "Validation Errors",
                    errors));
            }

            var createdLevel = await _levelService.CreateLevelAsync(levelCreateDto);

            return StatusCode((int)HttpStatusCode.Created,
                new ApiResponse<LevelEntityDto>(
                    (int)HttpStatusCode.Created,
                    "Level Created Successfully",
                    createdLevel));
        }

        // ================= GET ALL =================
        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<LevelEntityDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var levels = await _levelService.GetLevelsAsync();

                return Ok(new ApiResponse<IReadOnlyList<LevelEntityDto>>(
                    (int)HttpStatusCode.OK,
                    "Levels Retrieved Successfully",
                    levels));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching levels");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "Failed to retrieve levels",
                        ex.Message));
            }
        }

        // ================= GET BY ID =================
        [HttpGet("{levelId}")]
        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid levelId)
        {
            var level = await _levelService.GetLevelByIdAsync(levelId);

            if (level == null)
            {
                return NotFound(new ApiResponse<string>(
                    (int)HttpStatusCode.NotFound,
                    "Level Not Found",
                    string.Empty));
            }

            return Ok(new ApiResponse<LevelEntityDto>(
                (int)HttpStatusCode.OK,
                "Level Retrieved Successfully",
                level));
        }

        // ================= GET BY STAGE =================
        [HttpGet("by-stage/{stageId}")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<LevelEntityDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByStageIdAsync(Guid stageId)
        {
            try
            {
                var levels = await _levelService.GetLevelsByStageIdForAdminAsync(stageId);

                return Ok(new ApiResponse<IReadOnlyList<LevelEntityDto>>(
                    (int)HttpStatusCode.OK,
                    "Levels Retrieved Successfully",
                    levels));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving levels for stage {StageId}", stageId);

                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "Error retrieving levels",
                        ex.Message));
            }
        }

        // ================= UPDATE =================
        [HttpPut("update/{levelId}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ApiResponse<LevelEntityDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync(
            Guid levelId,
            [FromForm] LevelUpdateDto levelUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "Validation Errors",
                    errors));
            }

            var updatedLevel = await _levelService.UpdateLevelAsync(levelId, levelUpdateDto);

            if (updatedLevel == null)
            {
                return NotFound(new ApiResponse<string>(
                    (int)HttpStatusCode.NotFound,
                    "Level Not Found",
                    string.Empty));
            }

            return Ok(new ApiResponse<LevelEntityDto>(
                (int)HttpStatusCode.OK,
                "Level Updated Successfully",
                updatedLevel));
        }

        // ================= DELETE =================
        [HttpDelete("delete/{levelId}")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(Guid levelId)
        {
            var deleted = await _levelService.DeleteLevelAsync(levelId);

            if (!deleted)
            {
                return NotFound(new ApiResponse<string>(
                    (int)HttpStatusCode.NotFound,
                    "Level Not Found",
                    string.Empty));
            }

            return Ok(new ApiResponse<string>(
                (int)HttpStatusCode.OK,
                "Level Deleted Successfully",
                string.Empty));
        }
    }
}
