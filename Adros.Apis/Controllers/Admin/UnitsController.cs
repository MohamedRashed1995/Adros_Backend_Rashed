using Adros.Apis.ApiResponse;
//using Adros.Application.DTOs.Course;
using Adros.Application.DTOs.Topic;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController(IUnitService unitService, ILogger<UnitsController> logger) : BaseApiController
    {
        private readonly IUnitService _unitService = unitService;
        private readonly ILogger<UnitsController> _logger = logger;

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<UnitEntityDto>>>> GetAll()
        {
            var topics = await _unitService.GetAllUnitsAsync();
            return Ok(new ApiResponse<IReadOnlyList<UnitEntityDto>>((int)HttpStatusCode.OK, "Units retrieved successfully.", topics));
        }

        [HttpGet("{unitId}")]
        public async Task<ActionResult<ApiResponse<UnitEntityDto>>> GetById(Guid unitId)
        {
            var unit = await _unitService.GetUnitByIdAsync(unitId);
            if (unit == null) return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Unit not found.", string.Empty));
            return Ok(new ApiResponse<UnitEntityDto>((int)HttpStatusCode.OK, "Unit retrieved successfully.", unit));
        }

        //[HttpPost("create")]
        //public async Task<ActionResult<ApiResponse<TopicEntityDto>>> Create([FromBody] TopicCreateDto dto)
        //{
        //    var createdTopic = await _topicService.CreateTopicAsync(dto);
        //    return Ok(new ApiResponse<TopicEntityDto>((int)HttpStatusCode.Created, "Topic created successfully.", createdTopic));
        //}
        [HttpPost("create")]
        public async Task<IActionResult> Create(UnitCreateDto dto)
        {
            try
            {
                var result = await _unitService.CreateUnitAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }


        [HttpPut("{unitId}")]
        public async Task<ActionResult<ApiResponse<UnitEntityDto>>> Update(Guid topicId, [FromBody] UnitUpdateDto dto)
        {
            var updatedTopic = await _unitService.UpdateUnitAsync(topicId, dto);
            if (updatedTopic == null) return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Unit not found.", string.Empty));
            return Ok(new ApiResponse<UnitEntityDto>((int)HttpStatusCode.OK, "Unit updated successfully.", updatedTopic));
        }

        [HttpDelete("{unitId}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(Guid topicId)
        {
            var deleted = await _unitService.DeleteUnitAsync(topicId);
            if (!deleted) return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Unit not found.", string.Empty));
            return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Unit deleted successfully.", string.Empty));
        }

        [HttpGet("Get-Units-by-subject/{subjectId}")]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<UnitEntityDto>>>> GetBySubjectId(Guid subjectId)
        {
            var topics = await _unitService.GetUnitsBySubjectIdAsync(subjectId);

            return Ok(new ApiResponse<IReadOnlyList<UnitEntityDto>>(
                200,
                "Units retrieved successfully",
                topics
            ));
        }



    }
}
