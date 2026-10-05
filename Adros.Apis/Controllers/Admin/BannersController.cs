using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Banner;
using Adros.Application.DTOs.Stage;
using Adros.Application.Interfaces.IService;
using Adros.Application.Services.HomeService;
using Adros.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Admin
{

    //[Authorize(Roles = SystemRoles.Master)]
    public class BannersController(IBannerService bannerService , ILogger<BannersController> logger) : BaseApiController
    {
        private readonly IBannerService _bannerService = bannerService;
        private readonly ILogger<BannersController> _logger = logger;

        /// <summary>
        /// Creates a new banner.
        /// </summary>
        /// <param name="bannerCreateDto">The banner data to create.</param>
        /// <returns>The created banner details.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<BannerEntityDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BannerEntityDto>> CreateBannerAsync([FromForm] BannerCreateDto bannerCreateDto)
        {
            if (!ModelState.IsValid)
            {
                // Collect all validation errors
                var errors = string.Join("; ", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));

                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
            }

            try
            {
                var createdBanner = await _bannerService.CreateBannerAsync(bannerCreateDto);

                return Ok(
                           new ApiResponse<BannerEntityDto>((int)HttpStatusCode.Created, "Banner Created Successfully", createdBanner)
                );


            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                 _logger.LogError(ex, "Error creating banner.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while creating the banner.", string.Empty));
            }
        }

        /// <summary>
        /// Updates an existing banner.
        /// </summary>
        /// <param name="bannerId">The ID of the banner to update.</param>
        /// <param name="bannerUpdateDto">The updated banner data.</param>
        /// <returns>The updated banner details.</returns>
        [HttpPut("update/{bannerId}")]
        [ProducesResponseType(typeof(ApiResponse<ClientBannerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ClientBannerDto>> Update(Guid bannerId, [FromForm] BannerUpdateDto bannerUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                // Collect all validation errors
                var errors = string.Join("; ", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));

                return BadRequest(new ApiResponse<string>((int)HttpStatusCode.BadRequest, "Validation Errors", errors));
            }

            try
            {
                var updatedBanner = await _bannerService.UpdateBannerAsync(bannerId, bannerUpdateDto);
                if (updatedBanner == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Banner not found.", string.Empty));
                }

                return Ok(new ApiResponse<ClientBannerDto>((int)HttpStatusCode.OK, "Banner Updated Successfully", updatedBanner));
            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                 _logger.LogError(ex, "Error updating banner.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while updating the banner.", string.Empty));
            }
        }




        /// <summary>
        /// Retrieves a specific banner by its ID.
        /// </summary>
        /// <param name="bannerId">The ID of the banner to retrieve.</param>
        /// <returns>The requested banner details.</returns>
        [HttpGet("{bannerId}")]
        [ProducesResponseType(typeof(ApiResponse<BannerEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BannerEntityDto>> GetById(Guid bannerId)
        {
            try
            {
                var banner = await _bannerService.GetBannerByIdAsync(bannerId);
                if (banner == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Banner not found.", string.Empty));
                }

                return Ok(new ApiResponse<BannerEntityDto>((int)HttpStatusCode.OK, "Banner Retrieved Successfully", banner));
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error retrieving banner.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the banner.", string.Empty));
            }
        }


        /// <summary>
        /// Deletes an existing banner.
        /// </summary>
        /// <param name="bannerId">The ID of the banner to delete.</param>
        /// <returns>Status of the deletion.</returns>
        [HttpDelete("{bannerId}")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<string>> Delete(Guid bannerId)
        {
            try
            {
                var isDeleted = await _bannerService.GetBannerByIdAsync(bannerId);
                if (isDeleted == null)
                {
                    return NotFound(new ApiResponse<string>((int)HttpStatusCode.NotFound, "Banner not found.", string.Empty));
                }
                await _bannerService.DeleteBannerAsync(bannerId);
                return Ok(new ApiResponse<string>((int)HttpStatusCode.OK, "Banner deleted successfully.", string.Empty));
            }
            catch (Exception ex)
            {
                // Optionally, log the exception
                _logger.LogError(ex, "Error deleting banner.");

                // Return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the banner.", string.Empty));
            }
        }
        [HttpGet("Banners")]
        public async Task<ActionResult<IReadOnlyList<ClientStageDto>>> GetAllBannersAsync()
        {
            try
            {
                //var stages = await _stageService.GetClientStagesAsync();
                var banners = await _bannerService.GetClientBannersAsync();
                return Ok(new
                {
                    statusCode=HttpStatusCode.OK,
                    message="Banner List",
                    data=banners
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all Banners.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}
