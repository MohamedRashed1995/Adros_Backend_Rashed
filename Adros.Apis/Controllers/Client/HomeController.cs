using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Home;
using Adros.Application.DTOs.Level;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Client
{
    /// <summary>
    /// Controller for handling home page related operations
    /// </summary>
    public class HomeController(
        IBannerService bannerService,
        IStageService stageService,
        ICalenderService calenderService,
        ILogger<HomeController> logger) : BaseApiController
    {
        private readonly IBannerService _bannerService = bannerService;
        private readonly IStageService _stageService = stageService;
        private readonly ICalenderService _calenderService = calenderService;
        private readonly ILogger<HomeController> _logger = logger;

        /// <summary>
        /// Retrieves home page content including banners, stages, and calendar events
        /// </summary>
        /// <param name="startDate">Optional start date for calendar events filter (UTC)</param>
        /// <param name="endDate">Optional end date for calendar events filter (UTC)</param>
        /// <returns>Complete home page content</returns>
        /// <response code="200">Returns home page content</response>
        /// <response code="400">Invalid date range parameters</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<HomeResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HomeResponseDto>> GetHomeContentAsync(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            const string operation = nameof(GetHomeContentAsync);

            try
            {
                _logger.LogInformation("{Operation} initiated", operation);

                // Validate date range if both dates are provided
                if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                {
                    _logger.LogWarning("Invalid date range: {StartDate} to {EndDate}", startDate, endDate);
                    return BadRequest(new ApiResponse<string>(
                        (int)HttpStatusCode.BadRequest,
                        "Invalid date range",
                        "Start date must be before end date"));
                }

                var response = new HomeResponseDto
                {
                    Banners = await _bannerService.GetClientBannersAsync(),
                    Stages = await _stageService.GetClientStagesAsync(),
                    Calenders = await _calenderService.GetClientCalendersAsync(startDate, endDate)
                };

                _logger.LogInformation("{Operation} completed successfully", operation);

                return Ok(new ApiResponse<HomeResponseDto>(
                    (int)HttpStatusCode.OK,
                    "Home content retrieved successfully",
                    response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Operation} failed", operation);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "Error retrieving home content",
                        ex.Message));
            }
        }





    }
}