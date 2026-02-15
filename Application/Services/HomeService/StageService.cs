//using Adros.Application.DTOs.Stage;
//using Adros.Application.Interfaces.IService;
//using Adros.Core.DomainServices.IDomainService;
//using Adros.Core.Entities.Home;
//using Adros.Shared.Helpers;
//using Adros.Shared.Interfaces;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

//namespace Adros.Application.Services.HomeService
//{
//    public class StageService(IUnitOfWork unitOfWork,
//                             IMapper mapper,
//                             ILogger<StageService> logger,
//                             ICurrentUserService currentUserService,
//                             IHttpContextAccessor httpContextAccessor) : IStageService
//    {
//        private readonly IUnitOfWork _unitOfWork = unitOfWork;
//        private readonly IMapper _mapper = mapper;
//        private readonly ILogger<StageService> _logger = logger;
//        private readonly ICurrentUserService _currentUserService = currentUserService;
//        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

//        // ================= Helper =================
//        private string GetStageImageUrl(string imageName)
//        {
//            if (string.IsNullOrEmpty(imageName)) return string.Empty;
//            var request = _httpContextAccessor.HttpContext?.Request;
//            var baseUrl = request != null ? $"{request.Scheme}://{request.Host}" : string.Empty;
//            return $"{baseUrl}/Uploads/Images/Stages/{imageName}";
//        }

//        // ================= Get All =================
//        public async Task<IReadOnlyList<StageEntityDto>> GetClientStagesAsync()
//        {
//            var stages = await _unitOfWork.Repository<Stage>().ListAllAsync();

//            return stages.Select(stage => new StageEntityDto
//            {
//                Id = stage.Id,
//                Title = stage.Title,
//                Order = stage.Order,
//                Type = stage.Type,
//                CreatedBy = stage.CreatedBy,
//                UpdatedBy = stage.UpdatedBy,
//                CreatedAt = stage.CreatedAt,
//                UpdatedAt = stage.UpdatedAt,
//                ImagePath = GetStageImageUrl(stage.ImageName)
//            }).ToList();
//        }

//        // ================= Get By Id =================
//        public async Task<StageEntityDto?> GetStageByIdAsync(Guid stageId)
//        {
//            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
//            if (stage == null) return null;

//            var stageDto = _mapper.Map<StageEntityDto>(stage);
//            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);
//            return stageDto;
//        }

//        // ================= Create =================
//        public async Task<StageEntityDto> CreateStageAsync(StageCreateDto stageCreateDto)
//        {
//            var stage = _mapper.Map<Stage>(stageCreateDto);
//            stage.CreatedBy = _currentUserService.UserId != Guid.Empty ? _currentUserService.UserId : Guid.NewGuid();
//            stage.UpdatedBy = stage.CreatedBy;

//            if (stageCreateDto.Image != null && stageCreateDto.Image.Length > 0)
//            {
//                stage.ImageName = await FileManager.UploadFileAsync(stageCreateDto.Image, "Stages");
//            }
//            else
//            {
//                stage.ImageName = string.Empty;
//            }

//            await _unitOfWork.Repository<Stage>().AddAsync(stage);
//            await _unitOfWork.CompleteAsync();

//            var stageDto = _mapper.Map<StageEntityDto>(stage);
//            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);
//            return stageDto;
//        }

//        // ================= Update =================
//        public async Task<StageEntityDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto)
//        {
//            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
//            if (stage == null) return null;

//            if (!string.IsNullOrWhiteSpace(stageUpdateDto.Title))
//                stage.Title = stageUpdateDto.Title;
//            if (stageUpdateDto.Order.HasValue)
//                stage.Order = stageUpdateDto.Order;
//            if (stageUpdateDto.Type.HasValue)
//                stage.Type = stageUpdateDto.Type;

//            if (stageUpdateDto.Image != null && stageUpdateDto.Image.Length > 0)
//            {
//                try
//                {
//                    // حذف القديم إذا موجود
//                    if (!string.IsNullOrWhiteSpace(stage.ImageName))
//                        FileManager.DeleteFile(stage.ImageName, "Stages");

//                    // رفع الجديد
//                    stage.ImageName = await FileManager.UploadFileAsync(stageUpdateDto.Image, "Stages");
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogWarning(ex, "Image upload failed, stage updated without new image.");
//                }
//            }

//            stage.UpdatedAt = DateTime.Now;
//            stage.UpdatedBy = _currentUserService.UserId;

//            _unitOfWork.Repository<Stage>().Update(stage);
//            await _unitOfWork.CompleteAsync();

//            var stageDto = _mapper.Map<StageEntityDto>(stage);
//            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);
//            return stageDto;
//        }

//        // ================= Delete =================
//        public async Task<bool> DeleteStageAsync(Guid stageId)
//        {
//            var stage = await _unitOfWork.Repository<Stage>()
//                .GetFirstOrDefaultAsync(
//                    include: q => q.Include(s => s.Levels)
//                                   .ThenInclude(l => l.Subjects),
//                    filter: q => q.Where(s => s.Id == stageId)
//                );

//            if (stage == null) return false;

//            // حذف الصورة من السيرفر
//            if (!string.IsNullOrWhiteSpace(stage.ImageName))
//                FileManager.DeleteFile(stage.ImageName, "Stages");

//            _unitOfWork.Repository<Stage>().Delete(stage);
//            await _unitOfWork.CompleteAsync();

//            return true;
//        }
//    }
//}
using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.HomeService
{
    public class StageService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             ILogger<StageService> logger,
                             ICurrentUserService currentUserService,
                             IHttpContextAccessor httpContextAccessor) : IStageService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<StageService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        // ثابت موحد لمسار الصور
        private const string StageImagesFolder = "Images/Stages";

        // ================= Helper =================
        private string GetStageImageUrl(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = request != null
                ? $"{request.Scheme}://{request.Host}"
                : string.Empty;

            return $"{baseUrl}/Uploads/{StageImagesFolder}/{imageName}";
        }

        // ================= Get All =================
        public async Task<IReadOnlyList<StageEntityDto>> GetClientStagesAsync()
        {
            var stages = await _unitOfWork.Repository<Stage>().GetAllAsync(s => s 
                .Include(w => w.Teachers)
                .Include(w => w.Levels)
                .ThenInclude(w => w.Students)
            );

            return stages.Select(stage => new StageEntityDto
            {
                Id = stage.Id,
                Title = stage.Title,
                Order = stage.Order,
                Type = stage.Type,
                CreatedBy = stage.CreatedBy,
                UpdatedBy = stage.UpdatedBy,
                CreatedAt = stage.CreatedAt,
                UpdatedAt = stage.UpdatedAt,
                ImagePath = GetStageImageUrl(stage.ImageName),
                TeachersCount = stage.Teachers.Count,
                StudentsCount = stage.Levels.Sum(l => l.Students.Count)
            }).ToList();
        }

        // ================= Get By Id =================
        public async Task<StageEntityDto?> GetStageByIdAsync(Guid stageId)
        {
            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
            if (stage == null) return null;

            var stageDto = _mapper.Map<StageEntityDto>(stage);
            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);
            return stageDto;
        }

        // ================= Create =================
        public async Task<StageEntityDto> CreateStageAsync(StageCreateDto stageCreateDto)
        {
            var stage = _mapper.Map<Stage>(stageCreateDto);

            stage.CreatedBy = _currentUserService.UserId != Guid.Empty
                ? _currentUserService.UserId
                : Guid.NewGuid();

            stage.UpdatedBy = stage.CreatedBy;

            if (stageCreateDto.Image != null && stageCreateDto.Image.Length > 0)
            {
                // 👈 هنا التعديل المهم
                stage.ImageName = await FileManager.UploadFileAsync(
                    stageCreateDto.Image,
                    StageImagesFolder
                );
            }
            else
            {
                stage.ImageName = string.Empty;
            }

            await _unitOfWork.Repository<Stage>().AddAsync(stage);
            await _unitOfWork.CompleteAsync();

            var stageDto = _mapper.Map<StageEntityDto>(stage);
            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);

            return stageDto;
        }

        // ================= Update =================
        public async Task<StageEntityDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto)
        {
            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
            if (stage == null) return null;

            if (!string.IsNullOrWhiteSpace(stageUpdateDto.Title))
                stage.Title = stageUpdateDto.Title;

            if (stageUpdateDto.Order.HasValue)
                stage.Order = stageUpdateDto.Order;

            if (stageUpdateDto.Type.HasValue)
                stage.Type = stageUpdateDto.Type;

            if (stageUpdateDto.Image != null && stageUpdateDto.Image.Length > 0)
            {
                try
                {
                    // حذف القديم
                    if (!string.IsNullOrWhiteSpace(stage.ImageName))
                        FileManager.DeleteFile(stage.ImageName, StageImagesFolder);

                    // رفع الجديد
                    stage.ImageName = await FileManager.UploadFileAsync(
                        stageUpdateDto.Image,
                        StageImagesFolder
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Image upload failed, stage updated without new image.");
                }
            }

            stage.UpdatedAt = DateTime.Now;
            stage.UpdatedBy = _currentUserService.UserId;

            _unitOfWork.Repository<Stage>().Update(stage);
            await _unitOfWork.CompleteAsync();

            var stageDto = _mapper.Map<StageEntityDto>(stage);
            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);

            return stageDto;
        }

        // ================= Delete =================
        public async Task<bool> DeleteStageAsync(Guid stageId)
        {
            var stage = await _unitOfWork.Repository<Stage>()
                .GetFirstOrDefaultAsync(
                    include: q => q.Include(s => s.Levels)
                                   .ThenInclude(l => l.Subjects),
                    filter: q => q.Where(s => s.Id == stageId)
                );

            if (stage == null) return false;

            // حذف الصورة
            if (!string.IsNullOrWhiteSpace(stage.ImageName))
                FileManager.DeleteFile(stage.ImageName, StageImagesFolder);

            _unitOfWork.Repository<Stage>().Delete(stage);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
