using Adros.Application.DTOs.Calender;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
using Adros.Core.Enums;
using Adros.Core.Specifications;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.HomeService
{
    public class CalenderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CalenderService> logger,
        ICurrentUserService currentUserService
    ) : ICalenderService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CalenderService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<CalenderEntityDto> CreateCalenderAsync(CalenderCreateDto calenderCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating a new calendar entry for user: {UserId}", _currentUserService.UserId);

                // Validate color input
                if (!Enum.TryParse<CalenderColor>(calenderCreateDto.Color, true, out var parsedColor) ||
                    !Enum.IsDefined(parsedColor))
                {
                    var validColors = string.Join(", ", Enum.GetNames<CalenderColor>());
                    throw new ArgumentException($"Invalid calendar color. Valid values are: {validColors}");
                }

                var calender = _mapper.Map<Calender>(calenderCreateDto);
                calender.CreatedBy = _currentUserService.UserId;
                calender.Color = parsedColor; // Ensure parsed color is used

                await _unitOfWork.Repository<Calender>().AddAsync(calender);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully created calendar with ID: {CalenderId}", calender.Id);

                return _mapper.Map<CalenderEntityDto>(calender);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating calendar entry");
                throw;
            }
        }

        public async Task<CalenderEntityDto?> UpdateCalenderAsync(Guid calenderId, CalenderUpdateDto calenderUpdateDto)
        {
            try
            {
                _logger.LogInformation("Updating calendar with ID: {CalenderId} by user: {UserId}",
                    calenderId, _currentUserService.UserId);

                var calendar = await _unitOfWork.Repository<Calender>().GetByIdAsync(calenderId);
                if (calendar == null)
                {
                    _logger.LogWarning("Calendar with ID: {CalenderId} not found", calenderId);
                    return null;
                }

                // Validate color input
                if (!Enum.TryParse<CalenderColor>(calenderUpdateDto.Color, true, out var parsedColor) ||
                    !Enum.IsDefined(parsedColor))
                {
                    _logger.LogWarning("Invalid color value provided: {Color}", calenderUpdateDto.Color);
                    throw new ArgumentException($"Invalid color value. Valid values are: {string.Join(", ", Enum.GetNames<CalenderColor>())}");
                }

                // Update properties
                _mapper.Map(calenderUpdateDto, calendar);
                calendar.Color = parsedColor;
                calendar.UpdatedBy = _currentUserService.UserId;
                calendar.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<Calender>().Update(calendar);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully updated calendar with ID: {CalenderId}", calenderId);

                return _mapper.Map<CalenderEntityDto>(calendar);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Validation error updating calendar {CalenderId}", calenderId);
                throw; // Re-throw for controller handling
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating calendar with ID: {CalenderId}", calenderId);
                throw;
            }
        }

        public async Task<bool> DeleteCalenderAsync(Guid calenderId)
        {
            try
            {
                _logger.LogInformation("Deleting calender with ID: {CalenderId} by user: {UserId}", calenderId, _currentUserService.UserId);

                var calender = await _unitOfWork.Repository<Calender>().GetByIdAsync(calenderId);
                if (calender == null)
                {
                    _logger.LogWarning("Calender with ID: {CalenderId} not found", calenderId);
                    return false;
                }

                _unitOfWork.Repository<Calender>().Delete(calender);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully deleted calender with ID: {CalenderId}", calenderId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting calender with ID: {CalenderId}", calenderId);
                throw;
            }
        }

        public async Task<CalenderEntityDto?> GetCalenderByIdAsync(Guid calenderId)
        {
            try
            {
                _logger.LogInformation("Retrieving calender with ID: {CalenderId} by user: {UserId}", calenderId, _currentUserService.UserId);

                var calender = await _unitOfWork.Repository<Calender>().GetByIdAsync(calenderId);
                if (calender == null)
                {
                    _logger.LogWarning("Calender with ID: {CalenderId} not found", calenderId);
                    return null;
                }

                _logger.LogInformation("Successfully retrieved calender with ID: {CalenderId}", calenderId);

                return _mapper.Map<CalenderEntityDto>(calender);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving calender with ID: {CalenderId}", calenderId);
                throw;
            }
        }

        public async Task<IReadOnlyList<CalenderEntityDto>> GetAllCalendersAsync(
            string? color = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? skip = null,
            int? take = null)
        {
            try
            {
                _logger.LogInformation("Fetching calenders with filters - Color: {Color}, StartDate: {StartDate}, EndDate: {EndDate}, Skip: {Skip}, Take: {Take}",
                    color, startDate, endDate, skip, take);

                var spec = new CalenderSpecifications(
                    startDate:startDate,
                    endDate:endDate,
                    color:color,
                    skip:skip,
                    take:take);

                var calenders = await _unitOfWork.Repository<Calender>().ListAsync(spec);
                _logger.LogInformation("Successfully retrieved {Count} calenders", calenders.Count);

                return _mapper.Map<IReadOnlyList<Calender>, IReadOnlyList<CalenderEntityDto>>(calenders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching calenders");
                throw;
            }
        }

        public async Task<IReadOnlyList<ClientCalenderDto>> GetClientCalendersAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var userId = _currentUserService.UserId;
                _logger.LogInformation("Fetching client calenders for user: {UserId}, StartDate: {StartDate}, EndDate: {EndDate}", userId, startDate, endDate);

                var spec = new CalenderSpecifications(
                    userId,
                    startDate,
                    endDate
                );

                var calenders = await _unitOfWork.Repository<Calender>().ListAsync(spec);
                _logger.LogInformation("Successfully retrieved {Count} client calenders for user: {UserId}", calenders.Count, userId);

                return _mapper.Map<IReadOnlyList<Calender>, IReadOnlyList<ClientCalenderDto>>(calenders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching client calenders for user: {UserId}", _currentUserService.UserId);
                throw;
            }
        }
    }
}
