using Adros.Application.DTOs.Skills;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
<<<<<<< HEAD
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Shared.Interfaces;
using AutoMapper;

namespace Adros.Application.Services.HomeService
{
    public class SkillsService : ISkillsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public SkillsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

<<<<<<< HEAD
        public async Task<SkillShowDto> CreateAsync(SkillDto dto)
=======
        public async Task<SkillDto> CreateAsync(SkillDto dto)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        {
            var skill = _mapper.Map<Skill>(dto);

            skill.Id = Guid.NewGuid();
            skill.CreatedAt = DateTime.UtcNow;
            skill.CreatedBy = _currentUser.UserId;
<<<<<<< HEAD
            skill.Title = dto.title;
            skill.Description = dto.description;    
            skill.VideoURL = dto.videoURL;
            skill.ViewsCount = 0;
            await _unitOfWork.Repository<Skill>().AddAsync(skill);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SkillShowDto>(skill);
        }



=======

            await _unitOfWork.Repository<Skill>().AddAsync(skill);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SkillDto>(skill);
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<bool> DeleteAsync(Guid id)
        {
            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(id);
            if (skill == null) return false;

            _unitOfWork.Repository<Skill>().Delete(skill);
            await _unitOfWork.CompleteAsync();
            return true;
        }

<<<<<<< HEAD
        public async Task<IReadOnlyList<SkillShowDto>> GetAllAsync(int? take, int? skip)
        {
            var skills = await _unitOfWork.Repository<Skill>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<SkillShowDto>>(skills);
        }

        public async Task<SkillShowDto?> GetByIdAsync(Guid id)
        {
            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(id);
            return _mapper.Map<SkillShowDto>(skill);
        }

        public async Task<SkillDto?> UpdateAsync(Guid Id, SkillDto dto)
        {
            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(Id);
=======
        public async Task<IReadOnlyList<SkillDto>> GetAllAsync(int? take, int? skip)
        {
            var skills = await _unitOfWork.Repository<Skill>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<SkillDto>>(skills);
        }

        public async Task<SkillDto?> GetByIdAsync(Guid id)
        {
            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(id);
            return skill == null ? null : _mapper.Map<SkillDto>(skill);
        }

        public async Task<SkillDto?> UpdateAsync(SkillDto dto)
        {
            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(dto.id);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            if (skill == null) return null;

            _mapper.Map(dto, skill);
            skill.UpdatedAt = DateTime.UtcNow;
            skill.UpdatedBy = _currentUser.UserId;

            _unitOfWork.Repository<Skill>().Update(skill);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SkillDto>(skill);
        }
    }
}
