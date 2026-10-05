using Adros.Application.DTOs.Calender;

namespace Adros.Application.Interfaces.IService
{
    public interface ICalenderService
    {
        Task<CalenderEntityDto> CreateCalenderAsync(CalenderCreateDto calenderCreateDto);
        Task<CalenderEntityDto?> UpdateCalenderAsync(Guid calenderId, CalenderUpdateDto calenderUpdateDto);
        Task<bool> DeleteCalenderAsync(Guid calenderId);
        Task<CalenderEntityDto?> GetCalenderByIdAsync(Guid calenderId);
        Task<IReadOnlyList<CalenderEntityDto>> GetAllCalendersAsync(
            string? color = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? skip = null,
            int? take = null
        );

        Task<IReadOnlyList<ClientCalenderDto>> GetClientCalendersAsync(
            DateTime? startDate = null,
            DateTime? endDate = null
        );
    }
}
