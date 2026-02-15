using Adros.Application.DTOs.Skills;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
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

        public async Task<SkillShowDto> CreateAsync(SkillDto dto)
        {
            var skill = _mapper.Map<Skill>(dto);

            skill.Id = Guid.NewGuid();
            skill.CreatedAt = DateTime.UtcNow;
            skill.CreatedBy = _currentUser.UserId;
            skill.Title = dto.title;
            skill.Description = dto.description;    
            skill.VideoURL = dto.videoURL;
            skill.ViewsCount = 0;
            await _unitOfWork.Repository<Skill>().AddAsync(skill);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SkillShowDto>(skill);
        }



        public async Task<bool> DeleteAsync(Guid id)
        {
            var skill = await _unitOfWork.Repository<Skill>().GetByIdAsync(id);
            if (skill == null) return false;

            _unitOfWork.Repository<Skill>().Delete(skill);
            await _unitOfWork.CompleteAsync();
            return true;
        }

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
