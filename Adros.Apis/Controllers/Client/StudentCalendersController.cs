using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Calender;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Client
{
    /// <summary>
    /// Provides endpoints for managing student calendar entries
    /// </summary>
    [Authorize(Roles = SystemRoles.Student)]
    public class StudentCalendersController(
        ICalenderService calenderService,
        ILogger<StudentCalendersController> logger,
        ICurrentUserService currentUserService) : BaseApiController
    {
        private readonly ICalenderService _calenderService = calenderService;
        private readonly ILogger<StudentCalendersController> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        #region Calendar Retrieval Operations

        /// <summary>
        /// Retrieves calendar entries within a specified date range
        /// </summary>
        /// <param name="startDate">Optional start date filter (UTC)</param>
        /// <param name="endDate">Optional end date filter (UTC)</param>
        /// <returns>List of calendar entries matching the criteria</returns>
        /// <response code="200">Returns the list of calendar entries</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        //[ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ClientCalenderDto>>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ClientCalenderDto>>> GetMyCalender(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            const string operation = nameof(GetMyCalender);

            try
            {
                _logger.LogInformation("{Operation} initiated by {UserId}", operation, _currentUserService.UserId);

                var calenders = await _calenderService.GetClientCalendersAsync(startDate, endDate);

                _logger.LogInformation("{Operation} completed successfully. Found {Count} entries",
                    operation, calenders?.Count ?? 0);
                //new ApiResponse<IReadOnlyList<ClientCalenderDto>>(
                //    (int)HttpStatusCode.OK,
                //    "Calendar items retrieved successfully",
                return Ok(calenders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Operation} failed for user {UserId}", operation, _currentUserService.UserId);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "An error occurred while processing your request",
                        ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a specific calendar entry by ID
        /// </summary>
        /// <param name="calenderId">The ID of the calendar entry</param>
        /// <returns>The requested calendar entry</returns>
        /// <response code="200">Calendar entry found</response>
        /// <response code="404">Calendar entry not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{calenderId}")]
        [ProducesResponseType(typeof(ApiResponse<CalenderEntityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalenderEntityDto>> GetCalenderByIdAsync(Guid calenderId)
        {
            const string operation = nameof(GetCalenderByIdAsync);

            try
            {
                _logger.LogInformation("{Operation} requested for ID: {CalendarId}", operation, calenderId);

                var calender = await _calenderService.GetCalenderByIdAsync(calenderId);

                if (calender == null)
                {
                    _logger.LogWarning("{Operation} - Calendar {CalendarId} not found", operation, calenderId);
                    return NotFound(new ApiResponse<string>(
                        (int)HttpStatusCode.NotFound,
                        "Calendar item not found",
                        $"Calendar with ID {calenderId} does not exist"));
                }

                _logger.LogInformation("{Operation} retrieved calendar {CalendarId}", operation, calenderId);
                return Ok(new ApiResponse<CalenderEntityDto>(
                    (int)HttpStatusCode.OK,
                    "Calendar item retrieved successfully",
                    calender));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Operation} failed for ID {CalendarId}", operation, calenderId);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "An error occurred while processing your request",
                        ex.Message));
            }
        }

        #endregion

        #region Calendar Management Operations

        /// <summary>
        /// Creates a new calendar entry
        /// </summary>
        /// <param name="calenderCreateDto">Calendar creation data</param>
        /// <returns>Newly created calendar entry</returns>
        /// <response code="201">Calendar entry created successfully</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponse<CalenderEntityDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalenderEntityDto>> CreateCalenderAsync(
            [FromBody] CalenderCreateDto calenderCreateDto)
        {
            const string operation = nameof(CreateCalenderAsync);

            if (!ModelState.IsValid)
            {
                var errors = GetModelStateErrors();
                _logger.LogWarning("{Operation} validation failed: {Errors}", operation, errors);
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "Validation errors occurred",
                    errors));
            }

            try
            {
                var createdCalender = await _calenderService.CreateCalenderAsync(calenderCreateDto);

                _logger.LogInformation("{Operation} created new entry {CalendarId}",
                    operation, createdCalender?.Id);

                return Ok(
                    new ApiResponse<CalenderEntityDto>(
                        (int)HttpStatusCode.Created,
                        "Calendar item created successfully",
                        createdCalender ?? new CalenderEntityDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Operation} failed", operation);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "Creation failed",
                        ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing calendar entry
        /// </summary>
        /// <param name="calenderId">ID of the calendar entry to update</param>
        /// <param name="calenderUpdateDto">Updated calendar data</param>
        /// <returns>The updated calendar entry</returns>
        /// <response code="200">Calendar entry updated successfully</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="404">Calendar entry not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("Update/{calenderId}")]
        [ProducesResponseType(typeof(ApiResponse<CalenderEntityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalenderEntityDto>> UpdateCalenderAsync(
            Guid calenderId,
            [FromBody] CalenderUpdateDto calenderUpdateDto)
        {
            const string operation = nameof(UpdateCalenderAsync);

            if (!ModelState.IsValid)
            {
                var errors = GetModelStateErrors();
                _logger.LogWarning("{Operation} validation failed: {Errors}", operation, errors);
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "Validation errors occurred",
                    errors));
            }

            try
            {
                var updatedCalender = await _calenderService.UpdateCalenderAsync(calenderId, calenderUpdateDto);

                if (updatedCalender == null)
                {
                    _logger.LogWarning("{Operation} - Calendar {CalendarId} not found", operation, calenderId);
                    return NotFound(new ApiResponse<string>(
                        (int)HttpStatusCode.NotFound,
                        "Calendar item not found",
                        $"Calendar with ID {calenderId} does not exist"));
                }

                _logger.LogInformation("{Operation} updated calendar {CalendarId}", operation, calenderId);
                return Ok(new ApiResponse<CalenderEntityDto>(
                    (int)HttpStatusCode.OK,
                    "Calendar item updated successfully",
                    updatedCalender));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Operation} failed for ID {CalendarId}", operation, calenderId);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "Update failed",
                        ex.Message));
            }
        }

        /// <summary>
        /// Deletes a calendar entry
        /// </summary>
        /// <param name="calenderId">ID of the calendar entry to delete</param>
        /// <returns>Operation status</returns>
        /// <response code="200">Calendar entry deleted successfully</response>
        /// <response code="404">Calendar entry not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("Delete/{calenderId}")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCalenderAsync(Guid calenderId)
        {
            const string operation = nameof(DeleteCalenderAsync);

            try
            {
                _logger.LogInformation("{Operation} requested for ID: {CalendarId}", operation, calenderId);

                var result = await _calenderService.DeleteCalenderAsync(calenderId);

                if (!result)
                {
                    _logger.LogWarning("{Operation} - Calendar {CalendarId} not found", operation, calenderId);
                    return NotFound(new ApiResponse<string>(
                        (int)HttpStatusCode.NotFound,
                        "Calendar item not found",
                        $"Calendar with ID {calenderId} does not exist"));
                }

                _logger.LogInformation("{Operation} deleted calendar {CalendarId}", operation, calenderId);
                return Ok(new ApiResponse<string>(
                    (int)HttpStatusCode.OK,
                    "Calendar item deleted successfully",
                    string.Empty));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Operation} failed for ID {CalendarId}", operation, calenderId);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "Deletion failed",
                        ex.Message));
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Extracts and formats ModelState validation errors
        /// </summary>
        private string GetModelStateErrors()
        {
            return string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
        }

        #endregion
    }
}