using Adros.Application.DTOs.Level;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Course;
using Adros.Core.Specifications;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.HomeService
{

    public class LevelService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LevelService> logger,
        ICurrentUserService currentUserService, ISharedUserService sharedUserService) : ILevelService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<LevelService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ISharedUserService _sharedUserService = sharedUserService;

        public async Task<IReadOnlyList<ClientLevelDto>> GetLevelsForClientByStageIdAsync(Guid stageId)
        {
            try
            {
                _logger.LogInformation("Fetching client levels for stage ID: {StageId}", stageId);

                var spec = new LevelSpecifications(stageId);
                var levels = await _unitOfWork.Repository<Level>().ListAsync(spec);

                _logger.LogInformation("Successfully retrieved {Count} client levels for stage ID: {StageId}",
                    levels.Count, stageId);

                return _mapper.Map<IReadOnlyList<Level>, IReadOnlyList<ClientLevelDto>>(levels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching client levels for stage ID: {StageId}", stageId);
                throw;
            }
        }

        public async Task<IReadOnlyList<LevelEntityDto>> GetLevelsByStageIdForAdminAsync(Guid stageId)
        {
            _logger.LogInformation("Fetching admin levels for stage ID: {StageId}", stageId);

            var spec = new LevelSpecifications(stageId, includeDetails: true);
            var levels = await _unitOfWork.Repository<Level>().ListAsync(spec);

            _logger.LogInformation("Successfully retrieved {Count} admin levels for stage ID: {StageId}",
                levels.Count, stageId);

            var mappedLevels = _mapper.Map<IReadOnlyList<Level>, IReadOnlyList<LevelEntityDto>>(levels);

            foreach (var mappedLevel in mappedLevels)
            {
                if (!string.IsNullOrEmpty(mappedLevel.CreatedBy))
                {
                    mappedLevel.CreatedBy = await _sharedUserService.GetUserNameById(Guid.Parse(mappedLevel.CreatedBy));
                }
                if (!string.IsNullOrEmpty(mappedLevel.UpdatedBy))
                {
                    mappedLevel.UpdatedBy = await _sharedUserService.GetUserNameById(Guid.Parse(mappedLevel.UpdatedBy));
                }
            }

            return mappedLevels;
        }


        public async Task<LevelEntityDto> CreateLevelAsync(LevelCreateDto levelCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating new level by user: {UserId}", _currentUserService.UserId);

                var level = _mapper.Map<Level>(levelCreateDto);
                level.CreatedBy = _currentUserService.UserId;

                await _unitOfWork.Repository<Level>().AddAsync(level);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully created level with ID: {LevelId}", level.Id);

                var mappedDto = _mapper.Map<LevelEntityDto>(level);

                mappedDto.CreatedBy = await _sharedUserService.GetUserNameById(level.CreatedBy);
                if (level.UpdatedBy.HasValue)
                    mappedDto.UpdatedBy = await _sharedUserService.GetUserNameById(level.UpdatedBy.Value);

                return mappedDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating level");
                throw;
            }
        }

        public async Task<LevelEntityDto?> UpdateLevelAsync(Guid levelId, LevelUpdateDto levelUpdateDto)
        {
            try
            {
                _logger.LogInformation("Updating level ID: {LevelId} by user: {UserId}",
                    levelId, _currentUserService.UserId);

                var level = await _unitOfWork.Repository<Level>().GetByIdAsync(levelId);
                if (level == null)
                {
                    _logger.LogWarning("Level with ID: {LevelId} not found", levelId);
                    return null;
                }

                _mapper.Map(levelUpdateDto, level);
                level.UpdatedBy = _currentUserService.UserId;
                level.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<Level>().Update(level);
                await _unitOfWork.CompleteAsync();

                var mappedDto = _mapper.Map<LevelEntityDto>(level);

                mappedDto.CreatedBy = await _sharedUserService.GetUserNameById(level.CreatedBy);
                if (level.UpdatedBy.HasValue)
                    mappedDto.UpdatedBy = await _sharedUserService.GetUserNameById(level.UpdatedBy.Value);

                return mappedDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating level ID: {LevelId}", levelId);
                throw;
            }
        }

        public async Task<bool> DeleteLevelAsync(Guid levelId)
        {
            try
            {
                _logger.LogInformation("Deleting level ID: {LevelId} by user: {UserId}",
                    levelId, _currentUserService.UserId);

                var level = await _unitOfWork.Repository<Level>().GetByIdAsync(levelId);
                if (level == null)
                {
                    _logger.LogWarning("Level with ID: {LevelId} not found", levelId);
                    return false;
                }

                _unitOfWork.Repository<Level>().Delete(level);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully deleted level ID: {LevelId}", levelId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting level ID: {LevelId}", levelId);
                throw;
            }
        }

        public async Task<LevelEntityDto?> GetLevelByIdAsync(Guid levelId)
        {
            try
            {
                _logger.LogInformation("Fetching level ID: {LevelId} by user: {UserId}",
                    levelId, _currentUserService.UserId);

                var spec = new LevelSpecifications(levelId, includeDetails: true);
                var level = await _unitOfWork.Repository<Level>().GetEntityWithSpec(spec);

                if (level == null)
                {
                    _logger.LogWarning("Level with ID: {LevelId} not found", levelId);
                    return null;
                }
                var mappedDto = _mapper.Map<LevelEntityDto>(level);

                mappedDto.CreatedBy = await _sharedUserService.GetUserNameById(level.CreatedBy);
                if (level.UpdatedBy.HasValue)
                    mappedDto.UpdatedBy = await _sharedUserService.GetUserNameById(level.UpdatedBy.Value);

                _logger.LogInformation("Successfully retrieved level ID: {LevelId}", levelId);

                return mappedDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching level ID: {LevelId}", levelId);
                throw;
            }
        }
    }
}