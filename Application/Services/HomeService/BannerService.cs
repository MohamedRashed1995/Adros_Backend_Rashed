using Adros.Application.DTOs.Banner;
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
                return _mapper.Map<IReadOnlyList<Banner>, IReadOnlyList<ClientBannerDto>>(banners, opts =>
                {
                    opts.Items["FolderName"] = FoldersNames.Media.BannersFolder;
                });
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

                var banner= await _unitOfWork.Repository<Banner>().GetByIdAsync(bannerId);
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

    }
}
