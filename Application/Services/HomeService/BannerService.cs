using Adros.Application.DTOs.Banner;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
<<<<<<< HEAD
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
=======
using Adros.Core.Specifications;
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.HomeService
{

    public class BannerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BannerService> logger, ICurrentUserService currentUserService) : IBannerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<BannerService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IReadOnlyList<ClientBannerDto>> GetClientBannersAsync()
        {
            try
            {
                //var specs = new HomeBannerSpecifications();
                var banners = await _unitOfWork.Repository<Banner>().ListAllAsync();
                var mappedBanners = _mapper.Map<List<ClientBannerDto>>(banners, opts =>
                {
                    opts.Items["FolderName"] = FoldersNames.Media.BannersFolder;
                });

                return mappedBanners.AsReadOnly();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching client banners.");
                throw;
            }
        }


        public async Task<BannerEntityDto> CreateBannerAsync(BannerCreateDto bannerCreateDto)
        {

            var banner = _mapper.Map<Banner>(bannerCreateDto);

            // Assign CreatedBy and UpdatedBy
            banner.CreatedBy = _currentUserService.UserId;
            banner.UpdatedBy = _currentUserService.UserId;

            // Handle File Upload using FileManager
            if (bannerCreateDto.Image != null && bannerCreateDto.Image.Length > 0)
            {
                string folderName = FoldersNames.Media.BannersFolder;

                try
                {
                    // Upload the file asynchronously and get the unique filename
                    string uploadedFileName = await FileManager.UploadFileAsync(bannerCreateDto.Image, folderName);

                    // Set the ImageName property
                    banner.ImageName = uploadedFileName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading file.");
                    throw; // Optionally, wrap in a custom exception or handle accordingly
                }
            }

            // Add the new banner to the repository
            await _unitOfWork.Repository<Banner>().AddAsync(banner);
            await _unitOfWork.CompleteAsync();

            // Map the created entity to BannerEntityDto
            var bannerEntityDto = _mapper.Map<BannerEntityDto>(banner, opts =>
            {
                opts.Items["FolderName"] = FoldersNames.Media.BannersFolder;
            });

            return bannerEntityDto;
        }

        public async Task<ClientBannerDto?> UpdateBannerAsync(Guid bannerId, BannerUpdateDto bannerUpdateDto)
        {
            var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(bannerId);
            if (banner == null)
                return null;

            // Update Order if provided
            if (bannerUpdateDto.Order.HasValue)
                banner.Order = bannerUpdateDto.Order.Value;

            // Handle Image Replacement
            if (bannerUpdateDto.Image != null && bannerUpdateDto.Image.Length > 0)
            {
                string folderName = FoldersNames.Media.BannersFolder;

                try
                {
                    // Delete the old image if it exists
                    if (!string.IsNullOrWhiteSpace(banner.ImageName))
                    {
                        FileManager.DeleteFile(banner.ImageName, folderName);
                        banner.Deleted = true;
                    }

                    // Upload the new image
                    string uploadedFileName = await FileManager.UploadFileAsync(bannerUpdateDto.Image, folderName);

                    // Update the ImageName property
                    banner.ImageName = uploadedFileName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating banner image.");
                    throw;
                }
            }

            // Update the UpdatedAt and UpdatedBy properties
            banner.UpdatedAt = DateTime.Now;
            banner.UpdatedBy = _currentUserService.UserId;

            // Update the banner in the repository
            _unitOfWork.Repository<Banner>().Update(banner);
            await _unitOfWork.CompleteAsync();

            // Map the updated entity to ClientBannerDto
            var clientBannerDto = _mapper.Map<ClientBannerDto>(banner, opts =>
            {
                opts.Items["FolderName"] = FoldersNames.Media.BannersFolder;
            });

            return clientBannerDto;
        }

        public async Task<bool> DeleteBannerAsync(Guid bannerId)
        {
            try
            {
                _logger.LogInformation("Deleting banner with ID: {bannerid}", bannerId);

                var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(bannerId);
                //var banner =
                if (banner == null)
                {
                    _logger.LogWarning("Banner with ID: {bannerId} not found.", bannerId);
                    return false;
                }

                _unitOfWork.Repository<Banner>().Delete(banner);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully deleted Banner with ID: {bannerId}", bannerId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Banner with ID: {BannerId}", bannerId);
                throw;
            }
        }

        public async Task<BannerEntityDto?> GetBannerByIdAsync(Guid bannerId)
        {
            var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(bannerId);
            if (banner == null)
                return null;

            return _mapper.Map<BannerEntityDto>(banner, opts =>
            {
                opts.Items["FolderName"] = "banners";
            });
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
