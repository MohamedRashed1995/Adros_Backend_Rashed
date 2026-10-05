using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Skills;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
using Adros.Core.Specifications;
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.HomeService
{
    public class SkillsService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkillsService> logger, ICurrentUserService currentUserService) : ISkillsService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<SkillsService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<SkillDto> CreateVariousSkillAsync(SkillDto Skill)
        {
            if (string.IsNullOrWhiteSpace(Skill.Title) || string.IsNullOrWhiteSpace(Skill.Description))
            {
                throw new ArgumentException("Title and Description are required fields.");
            }

            try
            {
                // Map DTO to entity
                var skill = _mapper.Map<VariousSkill>(Skill);

                // Assign created/updated metadata
                skill.CreatedBy =  _currentUserService.UserId;
                skill.UpdatedBy = _currentUserService.UserId;
                skill.VideoURL = Skill.VideoURL;
                skill.Title = Skill.Title;
                skill.Description = Skill.Description;
                //skill.Views = Skill.ViewsCount;
                skill.Id = Skill.Id;

                // Add the skill to the repository
                var createdSkill = await _unitOfWork.Repository<VariousSkill>().AddAsync(skill);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Skill created successfully: {Title}", skill.Title);
                
                // Return the mapped SkillDto
                return _mapper.Map<SkillDto>(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new skill.");
                throw;
            }
        }

        public async Task<bool> DeleteVariousSkillAsync(Guid VariousSkillId)
        {
            var Skill = await _unitOfWork.Repository<VariousSkill>().GetByIdAsync(VariousSkillId);
            if (Skill == null)
                return false;

            // Soft delete the banner
            _unitOfWork.Repository<VariousSkill>().Delete(Skill);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IReadOnlyList<SkillDto>> GetVariousSkillAsync(int? take = null, int? skip = null)
        {
            try
            {
                var specs = new HomeSkillsSpecifications(take , skip);
                var Skills = await _unitOfWork.Repository<VariousSkill>().ListAsync(specs);
                return _mapper.Map<IReadOnlyList<VariousSkill>, IReadOnlyList<SkillDto>>(Skills);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Various Skills.");
                throw;
            }
        }

        public async Task<SkillDto?> GetVariousSkillByIdAsync(Guid VariousSkillId)
        {

            var Skill = await _unitOfWork.Repository<VariousSkill>().GetByIdAsync(VariousSkillId);
            if (Skill == null || Skill.Deleted == true)
                return null;

            return _mapper.Map<SkillDto>(Skill);
        }

        public async Task<SkillDto?> UpdateVariousSkillAsync( SkillDto SkillUpdateDto)
        {
            // Validate the input
            if (SkillUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(SkillUpdateDto));
            }

            if (string.IsNullOrWhiteSpace(SkillUpdateDto.Title) || string.IsNullOrWhiteSpace(SkillUpdateDto.Description))
            {
                throw new ArgumentException("Title and Description are required fields.");
            }

            try
            {
                // Retrieve the entity to update
                var skill = await _unitOfWork.Repository<VariousSkill>().GetByIdAsync(SkillUpdateDto.Id);
                if (skill == null)
                {
                    throw new KeyNotFoundException($"Skill with ID {SkillUpdateDto.Id} not found.");
                }

                // Map updated values from DTO to entity
                _mapper.Map(SkillUpdateDto, skill);

                // Update metadata
                skill.UpdatedBy = _currentUserService.UserId;
                //skill.
                // Persist the changes
                _unitOfWork.Repository<VariousSkill>().Update(skill);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Skill with ID {Id} updated successfully.", SkillUpdateDto.Id);

                // Return the updated SkillDto
                return _mapper.Map<SkillDto>(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the skill with ID {Id}.", SkillUpdateDto.Id);
                throw;
            }
        }
    }
}
