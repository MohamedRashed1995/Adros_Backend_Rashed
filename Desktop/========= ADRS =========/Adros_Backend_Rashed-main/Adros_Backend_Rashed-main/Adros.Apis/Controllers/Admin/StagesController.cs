//////using Adros.Apis.ApiResponse;
//////using Adros.Application.DTOs.Banner;
//////using Adros.Application.DTOs.Stage;
//////using Adros.Application.Interfaces.IService;
//////using Adros.Application.Services.HomeService;
//////using Adros.Shared.Constants;
//////using Microsoft.AspNetCore.Authorization;
//////using Microsoft.AspNetCore.Mvc;
//////using System.Net;
//////using System.ServiceModel.Channels;

//////namespace Adros.Apis.Controllers.Admin
//////{

//////    //[ApiController]
//////    //[Route("api/[controller]")]
//////    //public class StagesController(IStageService stageService, ILogger<StagesController> logger) : BaseApiController
//////    //{
//////    //    private readonly IStageService _stageService = stageService;
//////    //    private readonly ILogger<StagesController> _logger = logger;
//////    //    [HttpGet("all")]
//////    //    public async Task<ActionResult<ApiResponse<IReadOnlyList<StageEntityDto>>>> GetAllStagesAsync()
//////    //    {
//////    //        try
//////    //        {
//////    //            var stages = await _stageService.GetClientStagesAsync();

//////    //            if (stages == null || !stages.Any())
//////    //            {
//////    //                return Ok(new ApiResponse<IReadOnlyList<StageEntityDto>>(
//////    //                    (int)HttpStatusCode.OK,
//////    //                    "No stages found.",
//////    //                    new List<StageEntityDto>()
//////    //                ));
//////    //            }

//////    //            return Ok(new ApiResponse<IReadOnlyList<StageEntityDto>>(
//////    //                (int)HttpStatusCode.OK,
//////    //                "Stage list retrieved successfully.",
//////    //                stages
//////    //            ));
//////    //        }
//////    //        catch (Exception ex)
//////    //        {
//////    //            _logger.LogError(ex, "Error occurred while fetching all stages.");

//////    //            return StatusCode((int)HttpStatusCode.InternalServerError,
//////    //                new ApiResponse<string>(
//////    //                    (int)HttpStatusCode.InternalServerError,
//////    //                    "An error occurred while processing your request.",
//////    //                    ex.Message
//////    //                )
//////    //            );
//////    //        }
//////    //    }

//////    //    //[HttpPost("create")]
//////    //    //public async Task<ActionResult<ApiResponse<StageEntityDto>>> Create([FromForm] StageCreateDto stageCreateDto)
//////    //    //{
//////    //    //    if (!ModelState.IsValid)
//////    //    //    {
//////    //    //        var errors = string.Join("; ", ModelState.Values
//////    //    //                                        .SelectMany(x => x.Errors)
//////    //    //                                        .Select(x => x.ErrorMessage));
//////    //    //        return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
//////    //    //    }

//////    //    //    try
//////    //    //    {
//////    //    //        var createdStage = await _stageService.CreateStageAsync(stageCreateDto);

//////    //    //        return Ok(new ApiResponse<StageEntityDto>(
//////    //    //            (int)HttpStatusCode.Created,
//////    //    //            "Stage Created Successfully",
//////    //    //            createdStage
//////    //    //        ));
//////    //    //    }
//////    //    //    catch (Exception ex)
//////    //    //    {
//////    //    //        _logger.LogError(ex, "Error creating stage.");
//////    //    //        // ترجع response 200 مع رسالة لو حصل أي خطأ بدل 500
//////    //    //        return Ok(new ApiResponse<string>(
//////    //    //            (int)HttpStatusCode.OK,
//////    //    //            "Stage created with warning",
//////    //    //            "Image upload or mapping failed, stage created without image."
//////    //    //        ));
//////    //    //    }
//////    //    //}
//////    //    [HttpPost("create")]
//////    //    [ProducesResponseType(typeof(ApiResponse<BannerEntityDto>), (int)HttpStatusCode.Created)]
//////    //    [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
//////    //    public async Task<ActionResult<StageEntityDto>> Create([FromForm] StageCreateDto stageCreateDto)
//////    //    {
//////    //        if (!ModelState.IsValid)
//////    //        {
//////    //            // Collect all validation errors
//////    //            var errors = string.Join("; ", ModelState.Values
//////    //                                                    .SelectMany(x => x.Errors)
//////    //                                                    .Select(x => x.ErrorMessage));

//////    //            return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
//////    //        }

//////    //        try
//////    //        {
//////    //            var createdStage = await _stageService.CreateStageAsync(stageCreateDto);

//////    //            return Ok(
//////    //                       new ApiResponse<StageEntityDto>((int)HttpStatusCode.Created, "Stage Created Successfully", createdStage)
//////    //            );


//////    //        }
//////    //        catch (Exception ex)
//////    //        {
//////    //            // Optionally, log the exception
//////    //            _logger.LogError(ex, "Error creating Stage.");

//////    //            // Return a generic error response
//////    //            return StatusCode((int)HttpStatusCode.InternalServerError,
//////    //                new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while creating the stage.", string.Empty));
//////    //        }
//////    //    }


//////    //    [HttpPut("{stageId}")]
//////    //    public async Task<ActionResult<ClientStageDto>> Update(Guid stageId, [FromForm] StageUpdateDto stageUpdateDto)
//////    //    {
//////    //        if (!ModelState.IsValid)
//////    //        {
//////    //            // Collect all validation errors
//////    //            var errors = string.Join("; ", ModelState.Values
//////    //                                                    .SelectMany(x => x.Errors)
//////    //                                                    .Select(x => x.ErrorMessage));
//////    //            return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
//////    //        }

//////    //        try
//////    //        {
//////    //            var updatedStage = await _stageService.UpdateStageAsync(stageId, stageUpdateDto);
//////    //            if (updatedStage == null) return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "stage not found.", string.Empty));
//////    //            return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK,"Stage Updated Successfully.",updatedStage));
//////    //        }
//////    //        catch (Exception ex)
//////    //        {
//////    //            // Optionally, log the exception
//////    //            _logger.LogError(ex, "Error updating stage.");
//////    //            // Return a generic error response
//////    //            return StatusCode((int)HttpStatusCode.InternalServerError,
//////    //                new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while updating the stage.", string.Empty));
//////    //        }

//////    //    }

//////    //    /// <summary>
//////    //    /// Retrieves a specific stage by its ID.
//////    //    /// </summary>
//////    //    /// <param name="stageId">The ID of the stage to retrieve.</param>
//////    //    /// <returns>The requested stage details.</returns>
//////    //    [HttpGet("{stageId}")]
//////    //    [ProducesResponseType(typeof(ApiResponse<StageEntityDto>), (int)HttpStatusCode.OK)]
//////    //    [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
//////    //    public async Task<ActionResult<StageEntityDto>> GetById(Guid stageId)
//////    //    {
//////    //        try
//////    //        {
//////    //            var stage = await _stageService.GetStageByIdAsync(stageId);
//////    //            if (stage == null)
//////    //            {
//////    //                return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
//////    //            }

//////    //            return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK, "Stage Retrieved Successfully", stage));
//////    //        }
//////    //        catch (Exception ex)
//////    //        {
//////    //            // Log the exception
//////    //            _logger.LogError(ex, "Error retrieving Stage.");

//////    //            // Return a generic error response
//////    //            return StatusCode((int)HttpStatusCode.InternalServerError,
//////    //                new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the Stage.", string.Empty));
//////    //        }
//////    //    }

//////    //    /// <summary>
//////    //    /// Deletes an existing stage.
//////    //    /// </summary>
//////    //    /// <param name="stageId">The ID of the stage to delete.</param>
//////    //    /// <returns>Status of the deletion.</returns>
//////    //    [HttpDelete("{stageId}")]
//////    //    [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
//////    //    [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
//////    //    public async Task<ActionResult<string>> Delete(Guid stageId)
//////    //    {
//////    //        try
//////    //        {
//////    //            var isDeleted = await _stageService.DeleteStageAsync(stageId);
//////    //            if (!isDeleted)
//////    //            {
//////    //                return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
//////    //            }
//////    //            await _stageService.DeleteStageAsync(stageId);
//////    //            return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Stage deleted successfully.", string.Empty));
//////    //        }
//////    //        catch (Exception ex)
//////    //        {
//////    //            // Optionally, log the exception
//////    //            _logger.LogError(ex, "Error deleting Stage.");

//////    //            // Return a generic error response
//////    //            return StatusCode((int)HttpStatusCode.InternalServerError,
//////    //                new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the stage.", string.Empty));
//////    //        }
//////    //    }
//////    [ApiController]
//////    [Route("api/[controller]")]
//////    public class StagesController(IStageService stageService, ILogger<StagesController> logger) : BaseApiController
//////    {
//////        private readonly IStageService _stageService = stageService;
//////        private readonly ILogger<StagesController> _logger = logger;

//////        [HttpGet("all")]
//////        public async Task<ActionResult<ApiResponse<IReadOnlyList<StageEntityDto>>>> GetAllStagesAsync()
//////        {
//////            try
//////            {
//////                var stages = await _stageService.GetClientStagesAsync();
//////                return Ok(new ApiResponse<IReadOnlyList<StageEntityDto>>(
//////                    200,
//////                    "Stage list retrieved successfully.",
//////                    stages ?? new List<StageEntityDto>()
//////                ));
//////            }
//////            catch (Exception ex)
//////            {
//////                _logger.LogError(ex, "Error occurred while fetching all stages.");
//////                return Ok(new ApiResponse<string>(
//////                    200,
//////                    "Failed to fetch stages",
//////                    ex.Message
//////                ));
//////            }
//////        }

//////        [HttpPost("create")]
//////        public async Task<ActionResult<ApiResponse<StageEntityDto>>> Create([FromForm] StageCreateDto stageCreateDto)
//////        {
//////            if (!ModelState.IsValid)
//////            {
//////                var errors = string.Join("; ", ModelState.Values
//////                                                        .SelectMany(x => x.Errors)
//////                                                        .Select(x => x.ErrorMessage));
//////                return Ok(new ApiResponse<string>(200, "Stage creation failed", errors));
//////            }

//////            try
//////            {
//////                var stage = await _stageService.CreateStageAsync(stageCreateDto);
//////                return Ok(new ApiResponse<StageEntityDto>(200, "Stage created successfully", stage));
//////            }
//////            catch (Exception ex)
//////            {
//////                _logger.LogError(ex, "Error creating stage");
//////                return Ok(new ApiResponse<string>(200, "Stage creation failed", ex.Message));
//////            }
//////        }

//////        [HttpPut("{stageId}")]
//////        public async Task<ActionResult<ApiResponse<StageEntityDto>>> Update(Guid stageId, [FromForm] StageUpdateDto stageUpdateDto)
//////        {
//////            if (!ModelState.IsValid)
//////            {
//////                var errors = string.Join("; ", ModelState.Values
//////                                                        .SelectMany(x => x.Errors)
//////                                                        .Select(x => x.ErrorMessage));
//////                return Ok(new ApiResponse<string>(200, "Stage update failed", errors));
//////            }

//////            try
//////            {
//////                var stage = await _stageService.UpdateStageAsync(stageId, stageUpdateDto);
//////                if (stage == null)
//////                    return Ok(new ApiResponse<string>(200, "Stage update failed", "Stage not found."));

//////                return Ok(new ApiResponse<StageEntityDto>(200, "Stage updated successfully", stage));
//////            }
//////            catch (Exception ex)
//////            {
//////                _logger.LogError(ex, "Error updating stage");
//////                return Ok(new ApiResponse<string>(200, "Stage update failed", ex.Message));
//////            }
//////        }


//////        [HttpGet("{stageId}")]
//////        public async Task<ActionResult<ApiResponse<StageEntityDto>>> GetById(Guid stageId)
//////        {
//////            try
//////            {
//////                var stage = await _stageService.GetStageByIdAsync(stageId);
//////                if (stage == null)
//////                    return NotFound(new ApiResponse<string>(404, "Stage not found", string.Empty));

//////                return Ok(new ApiResponse<StageEntityDto>(200, "Stage Retrieved Successfully", stage));
//////            }
//////            catch (Exception ex)
//////            {
//////                _logger.LogError(ex, "Error retrieving stage.");
//////                return Ok(new ApiResponse<string>(200, "Stage retrieval failed", ex.Message));
//////            }
//////        }

//////        [HttpDelete("{stageId}")]
//////        public async Task<ActionResult<ApiResponse<string>>> Delete(Guid stageId)
//////        {
//////            try
//////            {
//////                var deleted = await _stageService.DeleteStageAsync(stageId);
//////                if (!deleted)
//////                    return Ok(new ApiResponse<string>(200, "Stage not found", string.Empty));

//////                return Ok(new ApiResponse<string>(200, "Stage deleted successfully", string.Empty));
//////            }
//////            catch (InvalidOperationException ex)
//////            {
//////                // هنا نرجع رسالة واضحة لو فيه Students مرتبطين
//////                return Ok(new ApiResponse<string>(200, "Stage deletion failed", ex.Message));
//////            }
//////            catch (Exception ex)
//////            {
//////                _logger.LogError(ex, "Error deleting stage.");
//////                return Ok(new ApiResponse<string>(200, "Stage deletion failed", ex.InnerException?.Message ?? ex.Message));
//////            }
//////        }




//////    }





//////}

////using Adros.Apis.ApiResponse;
////using Adros.Application.DTOs.Stage;
////using Adros.Application.Interfaces.IService;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.Extensions.Logging;
////using System;
////using System.Collections.Generic;
////using System.Net;
////using System.Threading.Tasks;

////namespace Adros.Apis.Controllers.Admin
////{
////    [ApiController]
////    [Route("api/[controller]")]
////    public class StagesController : ControllerBase
////    {
////        private readonly IStageService _stageService;
////        private readonly ILogger<StagesController> _logger;

////        public StagesController(IStageService stageService, ILogger<StagesController> logger)
////        {
////            _stageService = stageService;
////            _logger = logger;
////        }

////        [HttpGet("all")]
////        public async Task<ActionResult<ApiResponse<IReadOnlyList<StageEntityDto>>>> GetAllStagesAsync()
////        {
////            try
////            {
////                var stages = await _stageService.GetClientStagesAsync();
////                return Ok(new ApiResponse<IReadOnlyList<StageEntityDto>>(200, "Stage list retrieved successfully", stages));
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "Failed to fetch stages");
////                return Ok(new ApiResponse<string>(200, "Stage fetch failed", ex.Message));
////            }
////        }

////        [HttpPost("create")]
////        public async Task<ActionResult<ApiResponse<StageEntityDto>>> Create([FromForm] StageCreateDto dto)
////        {
////            try
////            {
////                var stage = await _stageService.CreateStageAsync(dto);
////                return Ok(new ApiResponse<StageEntityDto>(200, "Stage created successfully", stage));
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "Stage creation failed");
////                return Ok(new ApiResponse<string>(200, "Stage creation failed", ex.Message));
////            }
////        }

////        [HttpPut("{stageId}")]
////        public async Task<ActionResult<ApiResponse<StageEntityDto>>> Update(Guid stageId, [FromForm] StageUpdateDto dto)
////        {
////            try
////            {
////                var stage = await _stageService.UpdateStageAsync(stageId, dto);
////                if (stage == null)
////                    return Ok(new ApiResponse<string>(200, "Stage update failed", "Stage not found"));

////                return Ok(new ApiResponse<StageEntityDto>(200, "Stage updated successfully", stage));
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "Stage update failed");
////                return Ok(new ApiResponse<string>(200, "Stage update failed", ex.Message));
////            }
////        }

////        [HttpGet("{stageId}")]
////        public async Task<ActionResult<ApiResponse<StageEntityDto>>> GetById(Guid stageId)
////        {
////            try
////            {
////                var stage = await _stageService.GetStageByIdAsync(stageId);
////                if (stage == null) return Ok(new ApiResponse<string>(200, "Stage not found", string.Empty));

////                return Ok(new ApiResponse<StageEntityDto>(200, "Stage retrieved successfully", stage));
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "Stage retrieval failed");
////                return Ok(new ApiResponse<string>(200, "Stage retrieval failed", ex.Message));
////            }
////        }

////        [HttpDelete("{stageId}")]
////        public async Task<ActionResult<ApiResponse<string>>> Delete(Guid stageId)
////        {
////            try
////            {
////                var deleted = await _stageService.DeleteStageAsync(stageId);
////                if (!deleted) return Ok(new ApiResponse<string>(200, "Stage not found", string.Empty));

////                return Ok(new ApiResponse<string>(200, "Stage deleted successfully", string.Empty));
////            }
////            catch (Exception ex)
////            {
////                _logger.LogError(ex, "Stage deletion failed");
////                return Ok(new ApiResponse<string>(200, "Stage deletion failed", ex.InnerException?.Message ?? ex.Message));
////            }
////        }
////    }
////}

//using Adros.Apis.ApiResponse;
//using Adros.Application.DTOs.Stage;
//using Adros.Application.Interfaces.IService;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace Adros.Apis.Controllers.Admin
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class StagesController(IStageService stageService, ILogger<StagesController> logger) : BaseApiController
//    {
//        private readonly IStageService _stageService = stageService;
//        private readonly ILogger<StagesController> _logger = logger;

//        [HttpGet("all")]
//        public async Task<ActionResult<ApiResponse<IReadOnlyList<StageEntityDto>>>> GetAllStagesAsync()
//        {
//            try
//            {
//                var stages = await _stageService.GetClientStagesAsync();
//                return Ok(new ApiResponse<IReadOnlyList<StageEntityDto>>(200, "Stage list retrieved successfully", stages ?? new List<StageEntityDto>()));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching stages");
//                return Ok(new ApiResponse<string>(200, "Failed to fetch stages", ex.Message));
//            }
//        }

//        [HttpPost("create")]
//        public async Task<ActionResult<ApiResponse<StageEntityDto>>> Create([FromForm] StageCreateDto stageCreateDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                var errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
//                return Ok(new ApiResponse<string>(200, "Stage creation failed", errors));
//            }

//            try
//            {
//                var stage = await _stageService.CreateStageAsync(stageCreateDto);
//                return Ok(new ApiResponse<StageEntityDto>(200, "Stage created successfully", stage));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating stage");
//                return Ok(new ApiResponse<string>(200, "Stage creation failed", ex.Message));
//            }
//        }

//        [HttpPut("{stageId}")]
//        public async Task<ActionResult<ApiResponse<StageEntityDto>>> Update(Guid stageId, [FromForm] StageUpdateDto stageUpdateDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                var errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
//                return Ok(new ApiResponse<string>(200, "Stage update failed", errors));
//            }

//            try
//            {
//                var stage = await _stageService.UpdateStageAsync(stageId, stageUpdateDto);
//                if (stage == null) return Ok(new ApiResponse<string>(200, "Stage update failed", "Stage not found"));

//                return Ok(new ApiResponse<StageEntityDto>(200, "Stage updated successfully", stage));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error updating stage");
//                return Ok(new ApiResponse<string>(200, "Stage update failed", ex.Message));
//            }
//        }

//        [HttpGet("{stageId}")]
//        public async Task<ActionResult<ApiResponse<StageEntityDto>>> GetById(Guid stageId)
//        {
//            try
//            {
//                var stage = await _stageService.GetStageByIdAsync(stageId);
//                if (stage == null) return NotFound(new ApiResponse<string>(404, "Stage not found", string.Empty));

//                return Ok(new ApiResponse<StageEntityDto>(200, "Stage retrieved successfully", stage));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error retrieving stage");
//                return Ok(new ApiResponse<string>(200, "Stage retrieval failed", ex.Message));
//            }
//        }

//        [HttpDelete("{stageId}")]
//        public async Task<ActionResult<ApiResponse<string>>> Delete(Guid stageId)
//        {
//            try
//            {
//                var deleted = await _stageService.DeleteStageAsync(stageId);
//                if (!deleted) return Ok(new ApiResponse<string>(200, "Stage not found", string.Empty));

//                return Ok(new ApiResponse<string>(200, "Stage deleted successfully", string.Empty));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error deleting stage");
//                return Ok(new ApiResponse<string>(200, "Stage deletion failed", ex.InnerException?.Message ?? ex.Message));
//            }
//        }
//    }
//}


using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
    {
        private readonly IStageService _stageService;

        public StagesController(IStageService stageService)
        {
            _stageService = stageService;
        }

        // =================== GET: api/Stage ===================
        [HttpGet("all")]
        public async Task<IActionResult> GetAllStages()
        {
            var stages = await _stageService.GetClientStagesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Stages retrieved successfully",
                Data = stages
            });
        }

        // =================== GET: api/Stage/{id} ===================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStageById(Guid id)
        {
            var stage = await _stageService.GetStageByIdAsync(id);
            if (stage == null)
                return NotFound(new { StatusCode = 404, Message = "Stage not found" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Stage retrieved successfully",
                Data = stage
            });
        }

        // =================== POST: api/Stage ===================
        [HttpPost("create")]
        public async Task<IActionResult> CreateStage([FromForm] StageCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stage = await _stageService.CreateStageAsync(dto);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Stage created successfully",
                Data = stage
            });
        }

        // =================== PUT: api/Stage/{id} ===================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStage(Guid id, [FromForm] StageUpdateDto dto)
        {
            var stage = await _stageService.UpdateStageAsync(id, dto);
            if (stage == null)
                return NotFound(new { StatusCode = 404, Message = "Stage not found" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Stage updated successfully",
                Data = stage
            });
        }

        // =================== DELETE: api/Stage/{id} ===================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage(Guid id)
        {
            var deleted = await _stageService.DeleteStageAsync(id);
            if (!deleted)
                return NotFound(new { StatusCode = 404, Message = "Stage not found" });

            return Ok(new
            {
                StatusCode = 200,
                Message = "Stage deleted successfully"
            });
        }
    }
}
