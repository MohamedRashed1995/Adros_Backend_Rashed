using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Home;
using Adros.Core.Specifications;
using Adros.Shared.Constants;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Adros.Application.Services.HomeService
{



    public class StageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StageService> logger, ICurrentUserService currentUserService , IHttpContextAccessor httpContextAccessor) : IStageService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<StageService> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        //        public async Task<IReadOnlyList<ClientStageDto>> GetClientStagesAsync()
        //        {
        //            try
        //            {
        //                _logger.LogInformation("Fetching client stages...");
        //                var specs = new HomeStageSpecifications();
        //                var stages = await _unitOfWork.Repository<Stage>().ListAsync(specs);
        //                _logger.LogInformation("Successfully fetched {Count} client stages.", stages.Count);
        //                var mappedStages = _mapper.Map<IReadOnlyList<Stage>, IReadOnlyList<ClientStageDto>>(stages,
        //              opts =>
        //              {
        //                  opts.Items["FolderName"] = "Stages";
        //              }
        //);
        //                return mappedStages;
        //            }
        //            catch (Exception ex)
        //            {p[p
        //                _logger.LogError(ex, "Error occurred while fetching client stages.");
        //                throw;
        //            }
        //        }



        public async Task<IReadOnlyList<StageEntityDto>> GetClientStagesAsync()
        {
            try
            {
                var stages = await _unitOfWork.Repository<Stage>().ListAllAsync();
                return _mapper.Map<IReadOnlyList<Stage>, IReadOnlyList<StageEntityDto>>(stages, opts =>
                {
                    opts.Items["FolderName"] = FoldersNames.Media.StagesFolder;
                });
            }
                
    
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching client stages.");
                throw;
            }
        }


        public async Task<StageEntityDto> CreateStageAsync(StageCreateDto stageCreateDto)
        {

            var stage = _mapper.Map<Stage>(stageCreateDto);

            // Assign CreatedBy and UpdatedBy
            stage.CreatedBy = _currentUserService.UserId;
            stage.UpdatedBy = _currentUserService.UserId;

            // Handle File Upload using FileManager
            if (stageCreateDto.Image != null && stageCreateDto.Image.Length > 0)
            {
                string folderName = FoldersNames.Media.StagesFolder;

                try
                {
                    // Upload the file asynchronously and get the unique filename
                    string uploadedFileName = await FileManager.UploadFileAsync(stageCreateDto.Image, folderName);

                    // Set the ImageName property
                    stage.ImageName = uploadedFileName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading file.");
                    throw; // Optionally, wrap in a custom exception or handle accordingly
                }
            }

            // Add the new banner to the repository
            await _unitOfWork.Repository<Stage>().AddAsync(stage);
            await _unitOfWork.CompleteAsync();

            // Map the created entity to BannerEntityDto
            var stageDto = _mapper.Map<StageEntityDto>(stage, opts =>
            {
                opts.Items["FolderName"] = FoldersNames.Media.StagesFolder;
            });

            return stageDto;
        }

        public async Task<StageEntityDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto)
        {
            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
            if (stage == null)
            {
                return null;
            }
            if (stageUpdateDto.Order.HasValue)
            {
                stage.Order = stageUpdateDto.Order.Value;
            }

            if (stageUpdateDto.Image!= null && stageUpdateDto.Image.Length>0)
            {
                string folderName = FoldersNames.Media.StagesFolder;
                try
                {
                    //if (!string.IsNullOrWhiteSpace(stage.ImageName))
                    //{
                    //    FileManager.DeleteFile(stage.ImageName, folderName);
                    //    stage.Deleted = true;
                    //}
                    
                    string uploadedFileName = await FileManager.UploadFileAsync(stageUpdateDto.Image, folderName);
                    stage.ImageName = uploadedFileName;
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error Updating Stage Image");
                }
            }
            //stage.Title = stageUpdateDto.Title;
            stage.CreatedAt = DateTime.Now;
            stage.UpdatedAt = DateTime.Now;
            stage.CreatedBy = _currentUserService.UserId;
            stage.CreatedAt = DateTime.Now;

            _unitOfWork.Repository<Stage>().Update(stage);
            await _unitOfWork.CompleteAsync();

            // Map the updated entity to ClientBannerDto
            var clientStageDto = _mapper.Map<StageEntityDto>(stage, opts =>
            {
                opts.Items["FolderName"] = FoldersNames.Media.StagesFolder;
            });

            return clientStageDto;

        }

        //public async Task<ClientStageDto?> UpdateStageAsync(Guid stageId, StageUpdateDto stageUpdateDto)
        //{
        //    var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
        //    if (stage == null)
        //        return null;

        //    // Update Order if provided
        //    if (stageUpdateDto.Order.HasValue)
        //        stage.Order = stage.Order;

        //    if (!string.IsNullOrWhiteSpace(stageUpdateDto.Title))
        //    { 
        //        stage.Title = stageUpdateDto.Title;
        //    }

        //        // Handle Image Replacement
        //    if (stageUpdateDto.Image != null && stageUpdateDto.Image.Length > 0)
        //    {
        //        string folderName = FoldersNames.Media.StagesFolder;

        //        try
        //        {
        //            // Delete the old image if it exists
        //            if (!string.IsNullOrWhiteSpace(stage.ImageName))
        //            {
        //                FileManager.DeleteFile(stage.ImageName, folderName);
        //                stage.Deleted = true;
        //            }

        //            // Upload the new image
        //            string uploadedFileName = await FileManager.UploadFileAsync(stageUpdateDto.Image, folderName);

        //            // Update the ImageName property
        //            stage.ImageName = uploadedFileName;
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Error updating stage image.");
        //            throw;
        //        }
        //    }

        //    // Update the UpdatedAt and UpdatedBy properties


        //    stage.UpdatedAt = DateTime.Now;
        //    stage.UpdatedBy = _currentUserService.UserId;

        //    // Update the stage in the repository
        //    _unitOfWork.Repository<Stage>().Update(stage);
        //    await _unitOfWork.CompleteAsync();

        //    // Map the updated entity to StageEntityDto
        //    var clientstagedto = _mapper.Map<ClientStageDto>(stage, opts =>
        //    {
        //        opts.Items["FolderName"] = FoldersNames.Media.StagesFolder;
        //    });

        //    return clientstagedto;
        //}


        public async Task<bool> DeleteStageAsync(Guid stageId)
        {
            try
            {
                _logger.LogInformation("Deleting stage with ID: {StageId}", stageId);

                var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
                if (stage == null)
                {
                    _logger.LogWarning("Stage with ID: {StageId} not found.", stageId);
                    return false;
                }

                _unitOfWork.Repository<Stage>().Delete(stage);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully deleted stage with ID: {StageId}", stageId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting stage with ID: {StageId}", stageId);
                throw;
            }
        }

        public async Task<StageEntityDto?> GetStageByIdAsync(Guid stageId)
        {
            var stage = await _unitOfWork.Repository<Stage>().GetByIdAsync(stageId);
            if (stage == null)
                return null;

            return _mapper.Map<StageEntityDto>(stage, opts =>
            {
                opts.Items["FolderName"] = "Stages";
            });
        }
    }
}
