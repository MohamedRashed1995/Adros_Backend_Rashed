using Adros.Application.DTOs.Calender;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendersController(ICalenderService calenderService) : BaseApiController
    {
        private readonly ICalenderService _calenderService = calenderService;









        // Updated method to support filtering, sorting, and pagination
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CalenderEntityDto>>> GetAllCalendersAsync(
            [FromQuery] string? color = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var calenders = await _calenderService.GetAllCalendersAsync(color, startDate, endDate, skip, take);
            return Ok(calenders);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CalenderEntityDto>> GetCalenderById(Guid id)
        {
            var calender = await _calenderService.GetCalenderByIdAsync(id);
            if (calender == null) return NotFound($"Calendar with ID {id} not found.");
            return Ok(calender);
        }
        [HttpPost]
        public async Task<ActionResult<CalenderEntityDto>> CreateCalender([FromBody] CalenderCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var calender = await _calenderService.CreateCalenderAsync(createDto);
            return CreatedAtAction(nameof(GetCalenderById), new { id = calender.Id }, calender);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CalenderEntityDto>> UpdateCalender(Guid id, [FromBody] CalenderUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedCalender = await _calenderService.UpdateCalenderAsync(id, updateDto);
            if (updatedCalender == null) return NotFound($"Calendar with ID {id} not found.");

            return Ok(updatedCalender);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCalender(Guid id)
        {
            var deleted = await _calenderService.DeleteCalenderAsync(id);
            if (!deleted) return NotFound($"Calendar with ID {id} not found.");

            return NoContent();
        }
    }
}
