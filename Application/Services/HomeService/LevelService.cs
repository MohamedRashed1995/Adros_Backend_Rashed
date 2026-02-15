//using Adros.Application.DTOs.Level;
//using Adros.Application.DTOs.Stage;
//using Adros.Application.Interfaces.IService;
//using Adros.Core.DomainServices.IDomainService;
//using Adros.Core.Entities.Course;
//using Adros.Core.Entities.Home;
//using Adros.Core.Specifications;
//using Adros.Shared.Interfaces;
//using AutoMapper;
//using Microsoft.Extensions.Logging;

//namespace Adros.Application.Services.HomeService
//{

//    public class LevelService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LevelService> logger,
//        ICurrentUserService currentUserService, ISharedUserService sharedUserService) : ILevelService
//    {
//        private readonly IUnitOfWork _unitOfWork = unitOfWork;
//        private readonly IMapper _mapper = mapper;
//        private readonly ILogger<LevelService> _logger = logger;
//        private readonly ICurrentUserService _currentUserService = currentUserService;
//        private readonly ISharedUserService _sharedUserService = sharedUserService;



//        public async Task<IReadOnlyList<LevelEntityDto>> GetLevelsAsync()
//        {
//            var levels = await _unitOfWork.Repository<Level>().ListAllAsync();

//            var request = _httpContextAccessor.HttpContext?.Request;
//            var baseUrl = request != null ? $"{request.Scheme}://{request.Host}" : string.Empty;
//            var baseImageUrl = "/Images/Stages";

//            return levels.Select(level => new LevelEntityDto
//            {
//                Id = level.Id,
//                Title = level.Title,

//                CreatedBy = level.CreatedBy,
//                //UpdatedBy = stage.UpdatedBy,
//                CreatedAt = level.CreatedAt,
//                UpdatedAt = level.UpdatedAt,
//                StageName = level.Stage?.Title
//            }).ToList();
//        }

//        public async Task<IReadOnlyList<ClientLevelDto>> GetLevelsForClientByStageIdAsync(Guid stageId)
//        {
//            try
//            {
//                _logger.LogInformation("Fetching client levels for stage ID: {StageId}", stageId);

//                var spec = new LevelSpecifications(stageId);
//                var levels = await _unitOfWork.Repository<Level>().ListAsync(spec);

//                _logger.LogInformation("Successfully retrieved {Count} client levels for stage ID: {StageId}",
//                    levels.Count, stageId);

//                return _mapper.Map<IReadOnlyList<Level>, IReadOnlyList<ClientLevelDto>>(levels);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while fetching client levels for stage ID: {StageId}", stageId);
//                throw;
//            }
//        }

//        public async Task<IReadOnlyList<LevelEntityDto>> GetLevelsByStageIdForAdminAsync(Guid stageId)
//        {
//            _logger.LogInformation("Fetching admin levels for stage ID: {StageId}", stageId);

//            // Specification لجلب كل الـ Levels اللي في stageId ده مع التفاصيل
//            var spec = new LevelSpecifications(stageId, includeDetails: true);
//            var levels = await _unitOfWork.Repository<Level>().ListAsync(spec);

//            _logger.LogInformation("Successfully retrieved {Count} admin levels for stage ID: {StageId}", levels.Count, stageId);

//            // Mapping للـ DTO
//            var mappedLevels = _mapper.Map<IReadOnlyList<Level>, IReadOnlyList<LevelEntityDto>>(levels);

//            foreach (var mappedLevel in mappedLevels)
//            {
//                var entity = levels.FirstOrDefault(l => l.Id == mappedLevel.Id);
//                if (entity != null)
//                {
//                    mappedLevel.CreatedAt = entity.CreatedAt;
//                    mappedLevel.UpdatedAt = entity.UpdatedAt;
//                    mappedLevel.StageName = entity.StageName;
//                }

//                // معالجة CreatedBy



//                // معالجة UpdatedBy
//                if (!string.IsNullOrEmpty(mappedLevel.UpdatedBy)
//                    && Guid.TryParse(mappedLevel.UpdatedBy, out var updatedByGuid)
//                    && updatedByGuid != Guid.Empty)
//                {
//                    var updatedByName = await _sharedUserService.GetUserNameById(updatedByGuid);
//                    mappedLevel.UpdatedBy = string.IsNullOrEmpty(updatedByName) ? "Unknown User" : updatedByName;
//                }
//                else
//                {
//                    mappedLevel.UpdatedBy = "Unknown User";
//                }
//            }

//            return mappedLevels;
//        }





//        public async Task<LevelEntityDto> CreateLevelAsync(LevelCreateDto levelCreateDto)
//        {
//            try
//            {
//                _logger.LogInformation("Creating new level by user: {UserId}", _currentUserService.UserId);

//                var level = _mapper.Map<Level>(levelCreateDto);
//                level.CreatedBy = _currentUserService.UserId;

//                await _unitOfWork.Repository<Level>().AddAsync(level);
//                await _unitOfWork.CompleteAsync();

//                _logger.LogInformation("Successfully created level with ID: {LevelId}", level.Id);

//                var mappedDto = _mapper.Map<LevelEntityDto>(level);

//                //mappedDto.CreatedBy = await _sharedUserService.GetUserNameById(level.CreatedBy);
//                if (level.UpdatedBy.HasValue)
//                    mappedDto.UpdatedBy = await _sharedUserService.GetUserNameById(level.UpdatedBy.Value);

//                return mappedDto;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while creating level");
//                throw;
//            }
//        }

//        public async Task<LevelEntityDto?> UpdateLevelAsync(Guid levelId, LevelUpdateDto levelUpdateDto)
//        {
//            try
//            {
//                _logger.LogInformation("Updating level ID: {LevelId} by user: {UserId}",
//                    levelId, _currentUserService.UserId);

//                var level = await _unitOfWork.Repository<Level>().GetByIdAsync(levelId);
//                if (level == null)
//                {
//                    _logger.LogWarning("Level with ID: {LevelId} not found", levelId);
//                    return null;
//                }

//                _mapper.Map(levelUpdateDto, level);
//                level.UpdatedBy = _currentUserService.UserId;
//                level.UpdatedAt = DateTime.UtcNow;

//                _unitOfWork.Repository<Level>().Update(level);
//                await _unitOfWork.CompleteAsync();

//                var mappedDto = _mapper.Map<LevelEntityDto>(level);

//                //mappedDto.CreatedBy = await _sharedUserService.GetUserNameById(level.CreatedBy);
//                if (level.UpdatedBy.HasValue)
//                    mappedDto.UpdatedBy = await _sharedUserService.GetUserNameById(level.UpdatedBy.Value);

//                return mappedDto;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while updating level ID: {LevelId}", levelId);
//                throw;
//            }
//        }

//        public async Task<bool> DeleteLevelAsync(Guid levelId)
//        {
//            try
//            {
//                _logger.LogInformation("Deleting level ID: {LevelId} by user: {UserId}",
//                    levelId, _currentUserService.UserId);

//                var level = await _unitOfWork.Repository<Level>().GetByIdAsync(levelId);
//                if (level == null)
//                {
//                    _logger.LogWarning("Level with ID: {LevelId} not found", levelId);
//                    return false;
//                }

//                _unitOfWork.Repository<Level>().Delete(level);
//                await _unitOfWork.CompleteAsync();

//                _logger.LogInformation("Successfully deleted level ID: {LevelId}", levelId);

//                return true;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while deleting level ID: {LevelId}", levelId);
//                throw;
//            }
//        }

//        public async Task<LevelEntityDto?> GetLevelByIdAsync(Guid levelId)
//        {
//            try
//            {
//                _logger.LogInformation("Fetching level ID: {LevelId} by user: {UserId}",
//                    levelId, _currentUserService.UserId);

//                var spec = new LevelSpecifications(levelId, includeDetails: true);
//                var level = await _unitOfWork.Repository<Level>().GetEntityWithSpec(spec);

//                if (level == null)
//                {
//                    _logger.LogWarning("Level with ID: {LevelId} not found", levelId);
//                    return null;
//                }
//                var mappedDto = _mapper.Map<LevelEntityDto>(level);

//                //mappedDto.CreatedBy = await _sharedUserService.GetUserNameById(level.CreatedBy);
//                if (level.UpdatedBy.HasValue)
//                    mappedDto.UpdatedBy = await _sharedUserService.GetUserNameById(level.UpdatedBy.Value);

//                _logger.LogInformation("Successfully retrieved level ID: {LevelId}", levelId);

//                return mappedDto;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while fetching level ID: {LevelId}", levelId);
//                throw;
//            }
//        }
//    }
//}


using Adros.Application.DTOs.Level;
using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Course;
using Adros.Core.Entities.Home;
using Adros.Core.Entities.Users;
using Adros.Core.Specifications;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Data.Entity;

namespace Adros.Application.Services.HomeService
{
    public class LevelService : ILevelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LevelService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISharedUserService _sharedUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string LEVEL_IMAGE_FOLDER = "Images/Levels";

        public LevelService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<LevelService> logger,
            ICurrentUserService currentUserService,
            ISharedUserService sharedUserService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
            _sharedUserService = sharedUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        // ================= Helpers =================
        //private string BuildImageUrl(string? imageName)
        //{
        //    if (string.IsNullOrEmpty(imageName))
        //        return string.Empty;

        //    var request = _httpContextAccessor.HttpContext?.Request;
        //    if (request == null) return imageName;

        //    return $"{request.Scheme}://{request.Host}/{LEVEL_IMAGE_FOLDER}/{imageName}";
        //}
        private string GetLevelImageUrl(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = request != null
                ? $"{request.Scheme}://{request.Host}"
                : string.Empty;

            return $"{baseUrl}/Uploads/{LEVEL_IMAGE_FOLDER}/{imageName}";
        }

        // ================= GET ALL =================
        public async Task<IReadOnlyList<LevelEntityDto>> GetLevelsAsync()
        {
            // جلب كل المستويات مع Stage
            var levels = await _unitOfWork.Repository<Level>().GetAllAsync(
                include: q => q
                    .Include(l => l.Stage)
            );

            var levelDtos = new List<LevelEntityDto>();

            foreach (var level in levels)
            {
                // حساب عدد الطلاب لكل مستوى من قاعدة البيانات
                var studentsCount = await _unitOfWork.Repository<Student>()
                    .CountAsync(s => s.LevelId == level.Id);

                levelDtos.Add(new LevelEntityDto
                {
                    Id = level.Id,
                    Title = level.Title,
                    StageId = level.StageId,
                    ImagePath = GetLevelImageUrl(level.ImageName),
                    CreatedAt = level.CreatedAt,
                    UpdatedAt = level.UpdatedAt,
                    StageName = level.Stage?.Title,
                    CreatedBy = level.CreatedBy,
                    StudentsCount = studentsCount
                });
            }

            return levelDtos;
        }


        // ================= GET BY STAGE (CLIENT) =================
        public async Task<IReadOnlyList<ClientLevelDto>> GetLevelsForClientByStageIdAsync(Guid stageId)
        {
            var spec = new LevelSpecifications(stageId);
            var levels = await _unitOfWork.Repository<Level>().ListAsync(spec);

            var mapped = _mapper.Map<IReadOnlyList<ClientLevelDto>>(levels);

            foreach (var item in mapped)
            {
                item.Imagepath = GetLevelImageUrl(item.Imagepath);
            }

            return mapped;
        }

        // ================= GET BY STAGE (ADMIN) =================
        public async Task<IReadOnlyList<LevelEntityDto>> GetLevelsByStageIdForAdminAsync(Guid stageId)
        {
            var spec = new LevelSpecifications(stageId, includeDetails: true);
            var levels = await _unitOfWork.Repository<Level>().ListAsync(spec);

            var result = new List<LevelEntityDto>();

            foreach (var level in levels)
            {
                var dto = _mapper.Map<LevelEntityDto>(level);
                var studentsCount = await _unitOfWork.Repository<Student>()
                    .CountAsync(s => s.LevelId == level.Id);
                dto.ImagePath = GetLevelImageUrl(level.ImageName);
                dto.StageName = level.Stage?.Title;
                dto.CreatedAt = level.CreatedAt;
                dto.UpdatedAt = level.UpdatedAt;
                dto.Id = level.Id;
                dto.StageId = level.StageId;
                dto.Title = level.Title;
                dto.StudentsCount = studentsCount;
                result.Add(dto);
            }

            return result;
        }

        // ================= CREATE =================
        public async Task<LevelEntityDto> CreateLevelAsync(LevelCreateDto levelCreateDto)
        {
            
            var level = _mapper.Map<Level>(levelCreateDto);
            level.CreatedBy = _currentUserService.UserId != Guid.Empty ? _currentUserService.UserId : Guid.NewGuid();
            level.UpdatedBy = level.CreatedBy;

            if (levelCreateDto.Image != null && levelCreateDto.Image.Length > 0)
            {
                level.ImageName = await FileManager.UploadFileAsync(levelCreateDto.Image, LEVEL_IMAGE_FOLDER);
            }
            else
            {
                level.ImageName = string.Empty;
            }

            await _unitOfWork.Repository<Level>().AddAsync(level);
            await _unitOfWork.CompleteAsync();
            
            var dto = _mapper.Map<LevelEntityDto>(level);
            dto.ImagePath = GetLevelImageUrl(level.ImageName);
            dto.StageName = level.Stage?.Title;

            return dto;
        }

        // ================= UPDATE =================
        public async Task<LevelEntityDto?> UpdateLevelAsync(Guid levelId, LevelUpdateDto levelUpdateDto)
        {
            var level = await _unitOfWork.Repository<Level>().GetByIdAsync(levelId);
            if (level == null) return null;

            if (!string.IsNullOrWhiteSpace(levelUpdateDto.Title))
                level.Title = levelUpdateDto.Title;

            
            if (levelUpdateDto.Image != null && levelUpdateDto.Image.Length > 0)
            {
                try
                {
                    // حذف القديم
                    if (!string.IsNullOrWhiteSpace(level.ImageName))
                        FileManager.DeleteFile(level.ImageName, LEVEL_IMAGE_FOLDER);

                    // رفع الجديد
                    level.ImageName = await FileManager.UploadFileAsync(
                        levelUpdateDto.Image,
                        LEVEL_IMAGE_FOLDER
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Image upload failed, stage updated without new image.");
                }
            }

            level.UpdatedAt = DateTime.Now;
            level.UpdatedBy = _currentUserService.UserId;

            _unitOfWork.Repository<Level>().Update(level);
            await _unitOfWork.CompleteAsync();

            var levelDto = _mapper.Map<LevelEntityDto>(level);
            levelDto.ImagePath = GetLevelImageUrl(level.ImageName);

            return levelDto;
        }

        // ================= DELETE =================
        public async Task<bool> DeleteLevelAsync(Guid levelId)
        {
            var level = await _unitOfWork.Repository<Level>().GetByIdAsync(levelId);
            if (level == null) return false;

            _unitOfWork.Repository<Level>().Delete(level);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        // ================= GET BY ID =================
        public async Task<LevelEntityDto?> GetLevelByIdAsync(Guid levelId)
        {
            _logger.LogInformation("GetLevelByIdAsync started for LevelId: {LevelId}", levelId);

            // هنا نعمل Specification بشكل واضح
            var spec = new LevelSpecifications(levelId: levelId, includeDetails: true);
            _logger.LogInformation("LevelSpecifications created for LevelId: {LevelId}", levelId);

            // جلب الـ Level من DB
            var level = await _unitOfWork.Repository<Level>().GetEntityWithSpec(spec);

            if (level == null)
            {
                _logger.LogWarning("Level with Id {LevelId} not found in database", levelId);
                return null;
            }

            _logger.LogInformation("Level retrieved: {LevelTitle}, StageId: {StageId}", level.Title, level.StageId);

            // Mapping للـ DTO
            var dto = _mapper.Map<LevelEntityDto>(level);
            dto.ImagePath = GetLevelImageUrl(level.ImageName);
            dto.StageName = level.Stage?.Title;
            
            if (level.UpdatedBy.HasValue)
            {
                try
                {
                    dto.UpdatedBy = await _sharedUserService.GetUserNameById(level.UpdatedBy.Value);
                    _logger.LogInformation("UpdatedBy retrieved: {UserName}", dto.UpdatedBy);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching UpdatedBy user for LevelId: {LevelId}", levelId);
                }
            }

            _logger.LogInformation("GetLevelByIdAsync completed successfully for LevelId: {LevelId}", levelId);
            return dto;
        }

    }
}
