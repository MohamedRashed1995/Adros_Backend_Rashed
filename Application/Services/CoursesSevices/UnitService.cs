//using Adros.Application.DTOs.Course;
using Adros.Application.DTOs.Lesson;
using Adros.Application.DTOs.Topic;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Course;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.CourseService
{
    public class UnitService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UnitService> logger, ICurrentUserService currentUserService)
        : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<UnitService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IReadOnlyList<UnitEntityDto>> GetAllUnitsAsync()
        {
            var units= await _unitOfWork.Repository<Unit>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<UnitEntityDto>>(units);
        }

        public async Task<UnitEntityDto?> GetUnitByIdAsync(Guid unitid)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetByIdAsync(unitid);
            if (unit == null) return null;
            return _mapper.Map<UnitEntityDto>(unit);
        }

        public async Task<UnitEntityDto> CreateUnitAsync(UnitCreateDto dto)
        {
            var unit = _mapper.Map<Unit>(dto);

            unit.CreatedAt = DateTime.UtcNow;
            unit.UpdatedAt = DateTime.UtcNow;
            unit.CreatedBy = _currentUserService.UserId;
            unit.UpdatedBy = _currentUserService.UserId;

            await _unitOfWork.Repository<Unit>().AddAsync(unit);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UnitEntityDto>(unit);
        }







        public async Task<UnitEntityDto?> UpdateUnitAsync(Guid unitId, UnitUpdateDto dto)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetByIdAsync(unitId);
            if (unit == null) return null;

            _mapper.Map(dto, unit);
            unit.UpdatedBy = _currentUserService.UserId;
            unit.UpdatedAt = DateTime.Now;

            _unitOfWork.Repository<Unit>().Update(unit);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UnitEntityDto>(unit);
        }

        public async Task<bool> DeleteUnitAsync(Guid unitId)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetByIdAsync(unitId);
            if (unit == null) return false;

            _unitOfWork.Repository<Unit>().Delete(unit);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<IReadOnlyList<UnitEntityDto>> GetUnitsBySubjectIdAsync(Guid subjectId)
        {
            var spec = new UnitsBySubjectSpecification(subjectId);

            var units = await _unitOfWork
                .Repository<Unit>()
                .ListAsync(spec);

            return _mapper.Map<IReadOnlyList<UnitEntityDto>>(units);
        }

    }
}
