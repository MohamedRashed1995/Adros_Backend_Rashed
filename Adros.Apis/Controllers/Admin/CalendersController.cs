using Adros.Application.DTOs.Calender;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Admin
{
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
    }
}
