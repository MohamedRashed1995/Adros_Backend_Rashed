using Adros.Application.DTOs.Banner;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;



namespace Adros.Application.Services.HomeService
{
    public class BannerService : IBannerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BannerService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWebHostEnvironment _env;

        private const string BaseUrl = "https://adros-mrashed.runasp.net";

        // ⚠️ مسار نسبي فقط، يبدأ من wwwroot/Uploads
        private const string BannerRelativeFolder = "Images/Banners";

        private string BannerFolder => System.IO.Path.Combine(BannerRelativeFolder);

        public BannerService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<BannerService> logger,
            ICurrentUserService currentUserService,
            IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
            _env = env;

            // تأكد من وجود الفولدرات
            if (!System.IO.Directory.Exists(BannerFolder))
                System.IO.Directory.CreateDirectory(BannerFolder);

            // تهيئة FileManager
            FileManager.Init(_env);
            FileManager.EnsureFoldersExist();
        }

        // =================== CREATE ===================
        public async Task<BannerEntityDto> CreateBannerAsync(BannerCreateDto dto)
        {
            try
            {
                var banner = _mapper.Map<Banner>(dto);
                banner.CreatedBy = _currentUserService.UserId;
                banner.UpdatedBy = _currentUserService.UserId;

                if (dto.Image != null && dto.Image.Length > 0)
                {
                    banner.ImageName = await FileManager.UploadFileAsync(dto.Image, BannerFolder);
                    _logger.LogInformation("Banner saved at: " + System.IO.Path.Combine(BannerFolder, banner.ImageName));
                }

                await _unitOfWork.Repository<Banner>().AddAsync(banner);
                await _unitOfWork.CompleteAsync();

                return new BannerEntityDto
                {
                    Id = banner.Id,
                    Order = banner.Order,
                    ImageUrl = string.IsNullOrEmpty(banner.ImageName)
                            ? string.Empty
                            : $"{BaseUrl}/Uploads/{BannerRelativeFolder}/{banner.ImageName}"

                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating banner");
                throw new Exception("حدث خطأ أثناء إنشاء البنر");
            }
        }

        // =================== GET ALL ===================
        public async Task<IReadOnlyList<ClientBannerDto>> GetClientBannersAsync()
        {
            var banners = await _unitOfWork.Repository<Banner>().ListAllAsync();
            return banners.Select(b => new ClientBannerDto
            {
                Id = b.Id,
                Order = b.Order,
                ImageUrl = string.IsNullOrEmpty(b.ImageName)
                            ? string.Empty
                            : $"{BaseUrl}/Uploads/{BannerRelativeFolder}/{b.ImageName}"
            }).ToList().AsReadOnly();
        }

        // =================== GET BY ID ===================
        public async Task<BannerEntityDto?> GetBannerByIdAsync(Guid bannerId)
        {
            var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(bannerId);
            if (banner == null) return null;

            return new BannerEntityDto
            {
                Id = banner.Id,
                Order = banner.Order,
                ImageUrl = string.IsNullOrEmpty(banner.ImageName)
                    ? string.Empty
                    : $"{BaseUrl}/Uploads/{BannerRelativeFolder}/{banner.ImageName}"
            };
        }

        // =================== UPDATE ===================
        public async Task<ClientBannerDto?> UpdateBannerAsync(Guid bannerId, BannerUpdateDto dto)
        {
            var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(bannerId);
            if (banner == null) return null;

            if (dto.Order.HasValue)
                banner.Order = dto.Order.Value;

            if (dto.Image != null && dto.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(banner.ImageName))
                    FileManager.DeleteFile(BannerRelativeFolder, banner.ImageName);

                banner.ImageName = await FileManager.UploadFileAsync(dto.Image, BannerRelativeFolder);
            }

            banner.UpdatedAt = DateTime.UtcNow;
            banner.UpdatedBy = _currentUserService.UserId;

            _unitOfWork.Repository<Banner>().Update(banner);
            await _unitOfWork.CompleteAsync();

            return new ClientBannerDto
            {
                Id = banner.Id,
                Order = banner.Order,
                ImageUrl = string.IsNullOrEmpty(banner.ImageName)
                    ? string.Empty
                    : $"{BaseUrl}/Uploads/{BannerRelativeFolder}/{banner.ImageName}"
            };
        }

        // =================== DELETE ===================
        public async Task<bool> DeleteBannerAsync(Guid bannerId)
        {
            var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(bannerId);
            if (banner == null) return false;

            if (!string.IsNullOrEmpty(banner.ImageName))
                FileManager.DeleteFile(BannerRelativeFolder, banner.ImageName);

            _unitOfWork.Repository<Banner>().Delete(banner);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
