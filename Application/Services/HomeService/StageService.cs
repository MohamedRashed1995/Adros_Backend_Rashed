<<<<<<< HEAD
﻿//using Adros.Application.DTOs.Stage;
//using Adros.Application.Interfaces.IService;
//using Adros.Core.DomainServices.IDomainService;
=======
﻿////using Adros.Application.DTOs.Stage;
////using Adros.Application.Interfaces.IService;
////using Adros.Core.DomainServices.IDomainService;
////using Adros.Core.Entities.Home;
////using Adros.Core.Enums;
////using Adros.Shared.Constants;
////using Adros.Shared.Helpers;
////using Adros.Shared.Interfaces;
////using AutoMapper;
////using Microsoft.AspNetCore.Http;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.Extensions.Logging;
////using System.Linq;

////namespace Adros.Application.Services.HomeService
////{
////    public class StageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StageService> logger, ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor) : IStageService
////    {
////        private readonly IUnitOfWork _unitOfWork = unitOfWork;
////        private readonly IMapper _mapper = mapper;
////        private readonly ILogger<StageService> _logger = logger;
////        private readonly ICurrentUserService _currentUserService = currentUserService;
////        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

////        public async Task<IReadOnlyList<StageEntityDto>> GetClientStagesAsync()
////        {
////            var stages = await _unitOfWork.Repository<Stage>().ListAllAsync();

////            var request = _httpContextAccessor.HttpContext?.Request;
////            var baseUrl = request != null ? $"{request.Scheme}://{request.Host}" : string.Empty;
////            var baseImageUrl = "/Images/Stages";

////            return stages.Select(stage => new StageEntityDto
////            {
////                Id = stage.Id,
////                Title = stage.Title,
////                Order = stage.Order,
////                Type = stage.Type,
////                CreatedBy = stage.CreatedBy,
////                UpdatedBy = stage.UpdatedBy,
////                CreatedAt = stage.CreatedAt,
////                UpdatedAt = stage.UpdatedAt,
////                ImagePath = string.IsNullOrEmpty(stage.ImageName)
////                            ? string.Empty
////                            : $"{baseUrl}{baseImageUrl}/{stage.ImageName}"
////            }).ToList();
////        }

////        public async Task<StageEntityDto> CreateStageAsync(StageCreateDto stageCreateDto)
////        {
////            var stage = _mapper.Map<Stage>(stageCreateDto);

////            stage.CreatedBy = _currentUserService.UserId != Guid.Empty
////                    ? _currentUserService.UserId
////                    : Guid.NewGuid();

////            stage.UpdatedBy = stage.CreatedBy;


////            // ⬅️ ارفع الصورة الأول
////            if (stageCreateDto.Image != null && stageCreateDto.Image.Length > 0)
////            {
////                stage.ImageName = await FileManager.UploadFileAsync(stageCreateDto.Image, "Stages");
////            }
////            else
////            {
////                stage.ImageName = string.Empty; // أو خليه nullable
////            }

////            await _unitOfWork.Repository<Stage>().AddAsync(stage);
////            await _unitOfWork.CompleteAsync();

////            return _mapper.Map<StageEntityDto>(stage);
////        }


////        public async Task<StageEntityDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto)
////        {
////            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
////            if (stage == null) return null;

////            if (!string.IsNullOrWhiteSpace(stageUpdateDto.Title))
////                stage.Title = stageUpdateDto.Title;

////            if (stageUpdateDto.Order.HasValue)
////                stage.Order = stageUpdateDto.Order;

////            if (stageUpdateDto.Type.HasValue)
////                stage.Type = stageUpdateDto.Type;

////            if (stageUpdateDto.Image != null && stageUpdateDto.Image.Length > 0)
////            {
////                try
////                {
////                    stage.ImageName = await FileManager.UploadFileAsync(stageUpdateDto.Image, "Stages");
////                }
////                catch (Exception ex)
////                {
////                    _logger.LogWarning(ex, "Image upload failed, stage updated without new image.");
////                }
////            }

////            stage.UpdatedAt = DateTime.Now;
////            stage.UpdatedBy = _currentUserService.UserId;

////            _unitOfWork.Repository<Stage>().Update(stage);
////            await _unitOfWork.CompleteAsync();

////            return _mapper.Map<StageEntityDto>(stage);
////        }


////        public async Task<bool> DeleteStageAsync(Guid stageId)
////        {
////            // Include Levels و Subjects
////            var stage = await _unitOfWork.Repository<Stage>()
////                .GetFirstOrDefaultAsync(
////                    include: q => q.Include(s => s.Levels)
////                                   .ThenInclude(l => l.Subjects),
////                    filter: q => q.Where(s => s.Id == stageId)
////                );

////            if (stage == null) return false;

////            // 🔹 تحقق لو في أي Students مرتبطين بأي Level
////            var studentRepo = _unitOfWork.Repository<Core.Entities.Users.Student>();
////            var hasStudents = await studentRepo.Table
////                .AnyAsync(s => stage.Levels.Select(l => l.Id).Contains(s.LevelId));


////            if (hasStudents)
////            {
////                throw new InvalidOperationException("Cannot delete stage because there are students assigned to its levels.");
////            }

////            // 🔹 Hard delete مع Cascade
////            _unitOfWork.Repository<Stage>().Delete(stage);
////            await _unitOfWork.CompleteAsync();

////            return true;
////        }





////        public async Task<StageEntityDto?> GetStageByIdAsync(Guid stageId)
////        {
////            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
////            if (stage == null) return null;

////            return _mapper.Map<StageEntityDto>(stage, opts =>
////            {
////                opts.Items["FolderName"] = "Stages";
////            });
////        }
////    }
////}

//using Adros.Application.DTOs.Stage;
//using Adros.Application.Interfaces.IService;
//using Adros.Core.DomainServices.IDomainService;
//using Adros.Core.Entities.Course;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//using Adros.Core.Entities.Home;
//using Adros.Shared.Helpers;
//using Adros.Shared.Interfaces;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

//namespace Adros.Application.Services.HomeService
//{
<<<<<<< HEAD
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
=======
//    public class StageService : IStageService
//    {
//        private readonly IRepository<Stage> _stageRepository;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        private readonly ILogger<StageService> _logger;
//        private readonly ICurrentUserService _currentUserService;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public StageService(
//            IUnitOfWork unitOfWork,
//            IRepository<Stage> stageRepository,
//            IMapper mapper,
//            ILogger<StageService> logger,
//            ICurrentUserService currentUserService,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _unitOfWork = unitOfWork;
//            _stageRepository = stageRepository;
//            _mapper = mapper;
//            _logger = logger;
//            _currentUserService = currentUserService;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<IReadOnlyList<StageEntityDto>> GetClientStagesAsync()
//        {
//            var stages = await _stageRepository.ListAllAsync();

//            var request = _httpContextAccessor.HttpContext?.Request;
//            var baseUrl = request != null ? $"{request.Scheme}://{request.Host}" : string.Empty;
//            var baseImageUrl = "/Images/Stages";
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

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
<<<<<<< HEAD
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
=======
//                ImagePath = string.IsNullOrEmpty(stage.ImageName)
//                            ? string.Empty
//                            : $"{baseUrl}{baseImageUrl}/{stage.ImageName}"
//            }).ToList();
//        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//        public async Task<StageEntityDto> CreateStageAsync(StageCreateDto stageCreateDto)
//        {
//            var stage = _mapper.Map<Stage>(stageCreateDto);
//            stage.CreatedBy = _currentUserService.UserId != Guid.Empty ? _currentUserService.UserId : Guid.NewGuid();
//            stage.UpdatedBy = stage.CreatedBy;

//            if (stageCreateDto.Image != null && stageCreateDto.Image.Length > 0)
//            {
//                stage.ImageName = await FileManager.UploadFileAsync(stageCreateDto.Image, "Stages");
//            }
<<<<<<< HEAD
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
=======

//            await _stageRepository.AddAsync(stage);
//            await _unitOfWork.CompleteAsync();

//            return _mapper.Map<StageEntityDto>(stage);
//        }

//        public async Task<StageEntityDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto)
//        {
//            var stage = await _stageRepository.GetByIdAsync(stageId);
//            if (stage == null) return null;

//            if (!string.IsNullOrWhiteSpace(stageUpdateDto.Title)) stage.Title = stageUpdateDto.Title;
//            if (stageUpdateDto.Order.HasValue) stage.Order = stageUpdateDto.Order;
//            if (stageUpdateDto.Type.HasValue) stage.Type = stageUpdateDto.Type;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

//            if (stageUpdateDto.Image != null && stageUpdateDto.Image.Length > 0)
//            {
//                try
//                {
<<<<<<< HEAD
//                    // حذف القديم إذا موجود
//                    if (!string.IsNullOrWhiteSpace(stage.ImageName))
//                        FileManager.DeleteFile(stage.ImageName, "Stages");

//                    // رفع الجديد
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
//                    stage.ImageName = await FileManager.UploadFileAsync(stageUpdateDto.Image, "Stages");
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogWarning(ex, "Image upload failed, stage updated without new image.");
//                }
//            }

//            stage.UpdatedAt = DateTime.Now;
//            stage.UpdatedBy = _currentUserService.UserId;

<<<<<<< HEAD
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
=======
//            _stageRepository.Update(stage);
//            await _unitOfWork.CompleteAsync();

//            return _mapper.Map<StageEntityDto>(stage);
//        }

//        public async Task<bool> DeleteStageAsync(Guid stageId)
//        {
//            var stage = await _unitOfWork.Repository<Stage>()
//    .GetFirstOrDefaultAsync(
//        include: q => q.Include(s => s.Levels)
//                       .ThenInclude(l => l.Subjects), // Subjects ok
//        filter: q => q.Where(s => s.Id == stageId)
//    );

//            if (stage == null) return false;

//            // حذف كل Levels والـ Subjects المرتبطة قبل الحذف
//            foreach (var level in stage.Levels)
//            {
//                foreach (var subject in level.Subjects)
//                    _unitOfWork.Repository<Subject>().Delete(subject);

//                _unitOfWork.Repository<Level>().Delete(level);
//            }

//            _stageRepository.Delete(stage);
//            await _unitOfWork.CompleteAsync();
//            return true;
//        }

//        public async Task<StageEntityDto?> GetStageByIdAsync(Guid stageId)
//        {
//            var stage = await _stageRepository.GetByIdAsync(stageId);
//            if (stage == null) return null;

//            return _mapper.Map<StageEntityDto>(stage);
//        }
//    }
//}


>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
<<<<<<< HEAD
=======
using Adros.Core.Entities.Users;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.HomeService
{
<<<<<<< HEAD
    public class StageService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             ILogger<StageService> logger,
                             ICurrentUserService currentUserService,
                             IHttpContextAccessor httpContextAccessor) : IStageService
=======
    public class StageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StageService> logger, ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor) : IStageService
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<StageService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

<<<<<<< HEAD
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
=======
        public async Task<IReadOnlyList<StageEntityDto>> GetClientStagesAsync()
        {
            var stages = await _unitOfWork.Repository<Stage>().ListAllAsync();

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = request != null ? $"{request.Scheme}://{request.Host}" : string.Empty;
            var baseImageUrl = "/Images/Stages";
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

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
<<<<<<< HEAD
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

=======
                ImagePath = string.IsNullOrEmpty(stage.ImageName)
                            ? string.Empty
                            : $"{baseUrl}{baseImageUrl}/{stage.ImageName}"
            }).ToList();
        }

        public async Task<StageEntityDto> CreateStageAsync(StageCreateDto stageCreateDto)
        {
            var stage = _mapper.Map<Stage>(stageCreateDto);
            stage.CreatedBy = _currentUserService.UserId != Guid.Empty ? _currentUserService.UserId : Guid.NewGuid();
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            stage.UpdatedBy = stage.CreatedBy;

            if (stageCreateDto.Image != null && stageCreateDto.Image.Length > 0)
            {
<<<<<<< HEAD
                // 👈 هنا التعديل المهم
                stage.ImageName = await FileManager.UploadFileAsync(
                    stageCreateDto.Image,
                    StageImagesFolder
                );
=======
                stage.ImageName = await FileManager.UploadFileAsync(stageCreateDto.Image, "Stages");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            }
            else
            {
                stage.ImageName = string.Empty;
            }

            await _unitOfWork.Repository<Stage>().AddAsync(stage);
            await _unitOfWork.CompleteAsync();

<<<<<<< HEAD
            var stageDto = _mapper.Map<StageEntityDto>(stage);
            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);

            return stageDto;
        }

        // ================= Update =================
=======
            return _mapper.Map<StageEntityDto>(stage);
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<StageEntityDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto)
        {
            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
            if (stage == null) return null;

<<<<<<< HEAD
            if (!string.IsNullOrWhiteSpace(stageUpdateDto.Title))
                stage.Title = stageUpdateDto.Title;

            if (stageUpdateDto.Order.HasValue)
                stage.Order = stageUpdateDto.Order;

            if (stageUpdateDto.Type.HasValue)
                stage.Type = stageUpdateDto.Type;
=======
            if (!string.IsNullOrWhiteSpace(stageUpdateDto.Title)) stage.Title = stageUpdateDto.Title;
            if (stageUpdateDto.Order.HasValue) stage.Order = stageUpdateDto.Order;
            if (stageUpdateDto.Type.HasValue) stage.Type = stageUpdateDto.Type;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

            if (stageUpdateDto.Image != null && stageUpdateDto.Image.Length > 0)
            {
                try
                {
<<<<<<< HEAD
                    // حذف القديم
                    if (!string.IsNullOrWhiteSpace(stage.ImageName))
                        FileManager.DeleteFile(stage.ImageName, StageImagesFolder);

                    // رفع الجديد
                    stage.ImageName = await FileManager.UploadFileAsync(
                        stageUpdateDto.Image,
                        StageImagesFolder
                    );
=======
                    stage.ImageName = await FileManager.UploadFileAsync(stageUpdateDto.Image, "Stages");
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
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

<<<<<<< HEAD
            var stageDto = _mapper.Map<StageEntityDto>(stage);
            stageDto.ImagePath = GetStageImageUrl(stage.ImageName);

            return stageDto;
        }

        // ================= Delete =================
        public async Task<bool> DeleteStageAsync(Guid stageId)
        {
=======
            return _mapper.Map<StageEntityDto>(stage);
        }

        public async Task<bool> DeleteStageAsync(Guid stageId)
        {
            // Include Levels + Subjects (Cascade deletion will handle Students automatically)
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            var stage = await _unitOfWork.Repository<Stage>()
                .GetFirstOrDefaultAsync(
                    include: q => q.Include(s => s.Levels)
                                   .ThenInclude(l => l.Subjects),
                    filter: q => q.Where(s => s.Id == stageId)
                );

            if (stage == null) return false;

<<<<<<< HEAD
            // حذف الصورة
            if (!string.IsNullOrWhiteSpace(stage.ImageName))
                FileManager.DeleteFile(stage.ImageName, StageImagesFolder);

=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            _unitOfWork.Repository<Stage>().Delete(stage);
            await _unitOfWork.CompleteAsync();

            return true;
        }
<<<<<<< HEAD
=======

        public async Task<StageEntityDto?> GetStageByIdAsync(Guid stageId)
        {
            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
            if (stage == null) return null;

            return _mapper.Map<StageEntityDto>(stage, opts =>
            {
                opts.Items["FolderName"] = "Stages";
            });
        }
        //public async Task<StageEntityDto?> GetClientStageForStudentAsync(Guid studentId)
        //{
        //    var studentSpec = new StudentWithLevelSpec(studentId);
        //    var student = await _unitOfWork.Repository<Student>()
        //        .GetEntityWithSpec(studentSpec);

        //    if (student?.Level == null)
        //        return null;

        //    var stage = student.Level.Stage;
        //    return _mapper.Map<StageEntityDto>(stage);
        //}

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
