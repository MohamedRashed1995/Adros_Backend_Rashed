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

<<<<<<< HEAD
        public async Task<IReadOnlyList<UnitEntityDto>> GetAllUnitsAsync()
=======
        public async Task<IReadOnlyList<UnitEntityDto>> GetAllTopicsAsync()
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        {
            var units= await _unitOfWork.Repository<Unit>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<UnitEntityDto>>(units);
        }

<<<<<<< HEAD
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
=======
        public async Task<UnitEntityDto?> GetTopicByIdAsync(Guid topicId)
        {
            var topic = await _unitOfWork.Repository<Unit>().GetByIdAsync(topicId);
            if (topic == null) return null;
            return _mapper.Map<UnitEntityDto>(topic);
        }

        public async Task<UnitEntityDto> CreateTopicAsync(UnitCreateDto dto)
        {
            var topic = _mapper.Map<Unit>(dto);

            topic.CreatedAt = DateTime.UtcNow;
            topic.UpdatedAt = DateTime.UtcNow;
            topic.CreatedBy = _currentUserService.UserId;
            topic.UpdatedBy = _currentUserService.UserId;

            await _unitOfWork.Repository<Unit>().AddAsync(topic);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UnitEntityDto>(topic);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        }







<<<<<<< HEAD
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
=======
        public async Task<UnitEntityDto?> UpdateTopicAsync(Guid topicId, UnitUpdateDto dto)
        {
            var topic = await _unitOfWork.Repository<Unit>().GetByIdAsync(topicId);
            if (topic == null) return null;

            _mapper.Map(dto, topic);
            topic.UpdatedBy = _currentUserService.UserId;
            topic.UpdatedAt = DateTime.Now;

            _unitOfWork.Repository<Unit>().Update(topic);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UnitEntityDto>(topic);
        }

        public async Task<bool> DeleteTopicAsync(Guid topicId)
        {
            var topic = await _unitOfWork.Repository<Unit>().GetByIdAsync(topicId);
            if (topic == null) return false;

            _unitOfWork.Repository<Unit>().Delete(topic);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<IReadOnlyList<UnitEntityDto>> GetTopicsBySubjectIdAsync(Guid subjectId)
        {
            var spec = new UnitsBySubjectSpecification(subjectId);

            var topics = await _unitOfWork
                .Repository<Unit>()
                .ListAsync(spec);

            return _mapper.Map<IReadOnlyList<UnitEntityDto>>(topics);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        }

    }
}
