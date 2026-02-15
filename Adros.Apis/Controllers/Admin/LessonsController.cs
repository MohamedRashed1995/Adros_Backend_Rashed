<<<<<<< HEAD
﻿//using Adros.Apis.ApiResponse;
//using Adros.Application.DTOs;
//using Adros.Application.DTOs.Lesson;
//using Adros.Application.DTOs.Stage;
//using Adros.Application.Interfaces.IService;
//using Adros.Application.Services.HomeService;
//using Adros.Application.Services.UsersServices;
//using Adros.Core.Entities.Course;
//using Adros.Core.Specifications;
//using Adros.Persistence.UnitOfWork;
//using Adros.Shared.Constants;
//using Adros.Shared.Interfaces;
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.ServiceModel.Channels;

//namespace Adros.Apis.Controllers.Admin
//{

//    [Route("api/[controller]")]
//    [ApiController]
//    //[Route("api/lessons")]
//    public class LessonsController(ILessonsService lessonservice, ILogger<LessonsController> logger, IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
//    {
//        private readonly IUnitOfWork _unitOfWork = unitOfWork;
//        private readonly IMapper _mapper = mapper;
//        private readonly ILessonsService _lessonservice = lessonservice;
//        private readonly ILogger<LessonsController> _logger = logger;
//        [HttpGet("all")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetAllLessons()
//        {
//            var lessons = await _lessonservice.GetAllLessonsAsync();
//            return Ok(new
//            {
//                statusCode = 200,
//                message = "Lessons retrieved",
//                data = lessons
//            });
//        }

//        [HttpGet("by-subject/{subjectId}")]
//        public async Task<ActionResult<IReadOnlyList<LessonDto>>> GetBySubjectIdAsync(Guid subjectId)
//        {
//            var lessons = await _lessonservice.GetLessonsBySubjectIdAsync(subjectId);
//            return Ok(new ApiResponse<IReadOnlyList<LessonDto>>(200, "Lessons Retrieved", lessons));

//        }

//        [Authorize(Roles = "Teacher")]
//        [HttpPost("create")]
//        public async Task<ActionResult<LessonDto>> Create([FromBody] LessonCreateDto lessonCreateDto)
//        {
//            // ===== 1️⃣ Debug: Claims + Role =====
//            var userClaims = HttpContext.User.Claims.ToList();
//            foreach (var claim in userClaims)
//            {
//                _logger.LogInformation($"[Claims Debug] {claim.Type} = {claim.Value}");
//            }

//            _logger.LogInformation($"[Role Debug] Is user in Teacher role? {User.IsInRole("Teacher")}");

//            // ===== 2️⃣ Validate ModelState =====
//            if (!ModelState.IsValid)
//            {
//                var errors = string.Join("; ", ModelState.Values
//                                                        .SelectMany(x => x.Errors)
//                                                        .Select(x => x.ErrorMessage));

//                _logger.LogWarning("[Validation] " + errors);

//                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
//            }

//            try
//            {
//                // ===== 3️⃣ Create Lesson =====
//                var createdLesson = await _lessonservice.CreateLessonAsync(lessonCreateDto);

//                _logger.LogInformation($"[Success] Lesson created with Id: {createdLesson.Id}");

//                return Ok(new ApiResponse<LessonDto>((int)HttpStatusCode.OK, "Lesson Created Successfully", createdLesson));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating Lesson.");

//                return StatusCode((int)HttpStatusCode.InternalServerError,
//                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace));
//            }
//        }



//        //[HttpPut("{stageId}")]
//        //public async Task<ActionResult<ClientStageDto>> Update(Guid stageId, [FromForm] StageUpdateDto stageUpdateDto)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        // Collect all validation errors
//        //        var errors = string.Join("; ", ModelState.Values
//        //                                                .SelectMany(x => x.Errors)
//        //                                                .Select(x => x.ErrorMessage));
//        //        return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
//        //    }

//        //    try
//        //    {
//        //        var updatedStage = await _stageService.UpdateStageAsync(stageId, stageUpdateDto);
//        //        if (updatedStage == null) return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "stage not found.", string.Empty));
//        //        return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK, "Stage Updated Successfully.", updatedStage));
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Optionally, log the exception
//        //        _logger.LogError(ex, "Error updating stage.");
//        //        // Return a generic error response
//        //        return StatusCode((int)HttpStatusCode.InternalServerError,
//        //            new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while updating the stage.", string.Empty));
//        //    }

//        //}

//        ///// <summary>
//        ///// Retrieves a specific stage by its ID.
//        ///// </summary>
//        ///// <param name="stageId">The ID of the stage to retrieve.</param>
//        ///// <returns>The requested stage details.</returns>
//        //[HttpGet("{stageId}")]
//        //[ProducesResponseType(typeof(ApiResponse<StageEntityDto>), (int)HttpStatusCode.OK)]
//        //[ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
//        //public async Task<ActionResult<StageEntityDto>> GetById(Guid stageId)
//        //{
//        //    try
//        //    {
//        //        var stage = await _stageService.GetStageByIdAsync(stageId);
//        //        if (stage == null)
//        //        {
//        //            return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
//        //        }

//        //        return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK, "Stage Retrieved Successfully", stage));
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Log the exception
//        //        _logger.LogError(ex, "Error retrieving Stage.");

//        //        // Return a generic error response
//        //        return StatusCode((int)HttpStatusCode.InternalServerError,
//        //            new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the Stage.", string.Empty));
//        //    }
//        //}

//        ///// <summary>
//        ///// Deletes an existing stage.
//        ///// </summary>
//        ///// <param name="stageId">The ID of the stage to delete.</param>
//        ///// <returns>Status of the deletion.</returns>
//        //[HttpDelete("{stageId}")]
//        //[ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
//        //[ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
//        //public async Task<ActionResult<string>> Delete(Guid stageId)
//        //{
//        //    try
//        //    {
//        //        var isDeleted = await _stageService.DeleteStageAsync(stageId);
//        //        if (!isDeleted)
//        //        {
//        //            return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
//        //        }
//        //        await _stageService.DeleteStageAsync(stageId);
//        //        return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Stage deleted successfully.", string.Empty));
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Optionally, log the exception
//        //        _logger.LogError(ex, "Error deleting Stage.");

//        //        // Return a generic error response
//        //        return StatusCode((int)HttpStatusCode.InternalServerError,
//        //            new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the stage.", string.Empty));
//        //    }
//        //}

//        [HttpGet("{lessonId}")]
//        public async Task<ActionResult> GetById(Guid lessonId)
//        {
//            var lesson = await _lessonservice.GetLessonByIdAsync(lessonId);
//            if (lesson == null)
//                return NotFound(new ApiResponse<string>(404, "Lesson not found", string.Empty));

//            return Ok(new ApiResponse<LessonDto>(200, "Lesson Retrieved", lesson));
//        }


//        //[Authorize(Roles = SystemRoles.Teacher)]
//        [HttpPut("{lessonId}")]
//        public async Task<ActionResult> Update(Guid lessonId, [FromForm] LessonUpdateDto dto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var lesson = await _lessonservice.UpdateLessonAsync(lessonId, dto);
//            if (lesson == null)
//                return NotFound(new ApiResponse<string>(404, "Lesson not found", string.Empty));

//            return Ok(new ApiResponse<LessonDto>(200, "Lesson Updated Successfully", lesson));
//        }
//        //[Authorize(Roles = SystemRoles.Teacher)]
//        [HttpDelete("{lessonId}")]
//        public async Task<ActionResult> Delete(Guid lessonId)
//        {
//            var result = await _lessonservice.DeleteLessonAsync(lessonId);
//            if (!result)
//                return NotFound(new ApiResponse<string>(404, "Lesson not found", string.Empty));

//            return Ok(new ApiResponse<string>(200, "Lesson Deleted Successfully", string.Empty));
//        }
//        [HttpGet("Get-Lessons-by-UnitId/{unitId}")]
//        public async Task<ActionResult<ApiResponse<UnitWithLessonsDto>>> GetLessonsByUnit(Guid unitId)
//        {
//            var data = await _lessonservice.GetLessonsByUnitIdAsync(unitId);
//            return Ok(new ApiResponse<UnitWithLessonsDto>(200, "Lessons Retrieved", data));
//        }





//    }
//}


using Adros.Apis.ApiResponse;
using Adros.Application.DTOs;
using Adros.Application.DTOs.Lesson;
using Adros.Application.Interfaces.IService;
using Adros.Persistence.UnitOfWork;
=======
﻿using Adros.Apis.ApiResponse;
using Adros.Application.DTOs;
using Adros.Application.DTOs.Lesson;
using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Adros.Application.Services.HomeService;
using Adros.Application.Services.UsersServices;
using Adros.Core.Entities.Course;
using Adros.Core.Specifications;
using Adros.Persistence.UnitOfWork;
using Adros.Shared.Constants;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
<<<<<<< HEAD

namespace Adros.Apis.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILessonsService _lessonservice;
        private readonly ILogger<LessonsController> _logger;

        public LessonsController(
            ILessonsService lessonservice,
            ILogger<LessonsController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _lessonservice = lessonservice;
            _logger = logger;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLessons()
        {
            var lessons = await _lessonservice.GetAllLessonsAsync();
            return Ok(new
            {
                statusCode = 200,
                message = "Lessons retrieved",
                data = lessons
            });
        }
        [AllowAnonymous]
=======
using System.ServiceModel.Channels;

namespace Adros.Apis.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class LessonsController(ILessonsService lessonservice, ILogger<LessonsController> logger, IUnitOfWork unitOfWork, IMapper mapper) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILessonsService _lessonservice = lessonservice;
        private readonly ILogger<LessonsController> _logger = logger;
        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<LessonDto>>> GetAllLessonsAsync()
        {
            try
            {
                var lessons = await _lessonservice.GetAllLessonsAsync();
                //var stages = await _stageService.GetClientStagesAsync();
                return Ok(new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Lessons List",
                    data = lessons
                }
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all stages.");
                return StatusCode(500, new
                {
                    error = ex.Message,
                    stack = ex.StackTrace
                });
            }
        }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpGet("by-subject/{subjectId}")]
        public async Task<ActionResult<IReadOnlyList<LessonDto>>> GetBySubjectIdAsync(Guid subjectId)
        {
            var lessons = await _lessonservice.GetLessonsBySubjectIdAsync(subjectId);
            return Ok(new ApiResponse<IReadOnlyList<LessonDto>>(200, "Lessons Retrieved", lessons));
<<<<<<< HEAD
        }

        //[AllowAnonymous]
        //[Authorize(Roles = "Teacher")]
        [HttpPost("create")]
        public async Task<ActionResult<LessonDto>> Create([FromBody] LessonCreateDto lessonCreateDto)
        {
            // Debug: Claims + Role
            var userClaims = HttpContext.User.Claims.ToList();
            foreach (var claim in userClaims)
            {
                _logger.LogInformation($"[Claims Debug] {claim.Type} = {claim.Value}");
            }

            _logger.LogInformation($"[Role Debug] Is user in Teacher role? {User.IsInRole("Teacher")}");

            if (!ModelState.IsValid)
            {
=======
            
        }
        
        //[Authorize(Roles = SystemRoles.Teacher)]
        [HttpPost("create")]
        public async Task<ActionResult<LessonDto>> Create([FromForm] LessonCreateDto lessonCreateDto)
        {
            if (!ModelState.IsValid)
            {
                // Collect all validation errors
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                var errors = string.Join("; ", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));

<<<<<<< HEAD
                _logger.LogWarning("[Validation] " + errors);
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
            }

            try
            {
<<<<<<< HEAD
                var createdLesson = await _lessonservice.CreateLessonAsync(lessonCreateDto);
                _logger.LogInformation($"[Success] Lesson created with Id: {createdLesson.Id}");

                return Ok(new ApiResponse<LessonDto>((int)HttpStatusCode.OK, "Lesson Created Successfully", createdLesson));
            }
=======
                var createdlesson = await _lessonservice.CreateLessonAsync(lessonCreateDto);
                return Ok(
                           new ApiResponse<LessonDto>((int)HttpStatusCode.Created, "Lesson Created Successfully", createdlesson)
                );
            }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Lesson.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace));
            }
<<<<<<< HEAD
        }
        [AllowAnonymous]
        [   HttpGet("{lessonId:guid}")]
=======


        }

        //[HttpPut("{stageId}")]
        //public async Task<ActionResult<ClientStageDto>> Update(Guid stageId, [FromForm] StageUpdateDto stageUpdateDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // Collect all validation errors
        //        var errors = string.Join("; ", ModelState.Values
        //                                                .SelectMany(x => x.Errors)
        //                                                .Select(x => x.ErrorMessage));
        //        return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
        //    }

        //    try
        //    {
        //        var updatedStage = await _stageService.UpdateStageAsync(stageId, stageUpdateDto);
        //        if (updatedStage == null) return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "stage not found.", string.Empty));
        //        return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK, "Stage Updated Successfully.", updatedStage));
        //    }
        //    catch (Exception ex)
        //    {
        //        // Optionally, log the exception
        //        _logger.LogError(ex, "Error updating stage.");
        //        // Return a generic error response
        //        return StatusCode((int)HttpStatusCode.InternalServerError,
        //            new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while updating the stage.", string.Empty));
        //    }

        //}

        ///// <summary>
        ///// Retrieves a specific stage by its ID.
        ///// </summary>
        ///// <param name="stageId">The ID of the stage to retrieve.</param>
        ///// <returns>The requested stage details.</returns>
        //[HttpGet("{stageId}")]
        //[ProducesResponseType(typeof(ApiResponse<StageEntityDto>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult<StageEntityDto>> GetById(Guid stageId)
        //{
        //    try
        //    {
        //        var stage = await _stageService.GetStageByIdAsync(stageId);
        //        if (stage == null)
        //        {
        //            return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
        //        }

        //        return Ok(new ApiResponse<StageEntityDto>((int)HttpStatusCode.OK, "Stage Retrieved Successfully", stage));
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        _logger.LogError(ex, "Error retrieving Stage.");

        //        // Return a generic error response
        //        return StatusCode((int)HttpStatusCode.InternalServerError,
        //            new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the Stage.", string.Empty));
        //    }
        //}

        ///// <summary>
        ///// Deletes an existing stage.
        ///// </summary>
        ///// <param name="stageId">The ID of the stage to delete.</param>
        ///// <returns>Status of the deletion.</returns>
        //[HttpDelete("{stageId}")]
        //[ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult<string>> Delete(Guid stageId)
        //{
        //    try
        //    {
        //        var isDeleted = await _stageService.DeleteStageAsync(stageId);
        //        if (!isDeleted)
        //        {
        //            return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Stage not found.", string.Empty));
        //        }
        //        await _stageService.DeleteStageAsync(stageId);
        //        return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Stage deleted successfully.", string.Empty));
        //    }
        //    catch (Exception ex)
        //    {
        //        // Optionally, log the exception
        //        _logger.LogError(ex, "Error deleting Stage.");

        //        // Return a generic error response
        //        return StatusCode((int)HttpStatusCode.InternalServerError,
        //            new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the stage.", string.Empty));
        //    }
        //}

        [HttpGet("{lessonId}")]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<ActionResult> GetById(Guid lessonId)
        {
            var lesson = await _lessonservice.GetLessonByIdAsync(lessonId);
            if (lesson == null)
                return NotFound(new ApiResponse<string>(404, "Lesson not found", string.Empty));

            return Ok(new ApiResponse<LessonDto>(200, "Lesson Retrieved", lesson));
        }
<<<<<<< HEAD
        [AllowAnonymous]
=======

        //[Authorize(Roles = SystemRoles.Teacher)]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpPut("{lessonId}")]
        public async Task<ActionResult> Update(Guid lessonId, [FromForm] LessonUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lesson = await _lessonservice.UpdateLessonAsync(lessonId, dto);
            if (lesson == null)
                return NotFound(new ApiResponse<string>(404, "Lesson not found", string.Empty));

            return Ok(new ApiResponse<LessonDto>(200, "Lesson Updated Successfully", lesson));
        }
<<<<<<< HEAD

=======
        //[Authorize(Roles = SystemRoles.Teacher)]
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        [HttpDelete("{lessonId}")]
        public async Task<ActionResult> Delete(Guid lessonId)
        {
            var result = await _lessonservice.DeleteLessonAsync(lessonId);
            if (!result)
                return NotFound(new ApiResponse<string>(404, "Lesson not found", string.Empty));

            return Ok(new ApiResponse<string>(200, "Lesson Deleted Successfully", string.Empty));
        }
<<<<<<< HEAD
        [AllowAnonymous]
        [HttpGet("Get-Lessons-by-UnitId/{unitId}")]
        public async Task<ActionResult<ApiResponse<UnitWithLessonsDto>>> GetLessonsByUnit(Guid unitId)
        {
            var data = await _lessonservice.GetLessonsByUnitIdAsync(unitId);
            return Ok(new ApiResponse<UnitWithLessonsDto>(200, "Lessons Retrieved", data));
        }
=======
        [HttpGet("Get-Lessons-by-UnitId/{unitId}")]
        public async Task<ActionResult<ApiResponse<List<LessonDto>>>> GetLessonsByTopic(Guid unitid)
        {
            var lessons = await _lessonservice.GetLessonsByTopicIdAsync(unitid);

            // لو lessons نوعه IReadOnlyList
            var lessonsList = lessons.ToList();

            return Ok(new ApiResponse<List<LessonDto>>(200, "Lessons Retrieved", lessonsList));
        }




>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
