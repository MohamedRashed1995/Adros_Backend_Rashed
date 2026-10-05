using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Assesment;
using Adros.Application.DTOs.Banner;
using Adros.Application.Interfaces.IService;
using Adros.Application.Services.HomeService;
using Adros.Core.Entities.Assessements;
using Adros.Shared.Constants;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adros.Apis.Controllers.Assesments
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SystemRoles.Teacher)]
    public class AssesmentController(IAssesmentService assesmentService, ILogger<AssesmentController> logger) : ControllerBase
    {
        private readonly IAssesmentService _iassessmentservice = assesmentService;
        private readonly ILogger<AssesmentController> _logger = logger;
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<AssesmentDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Assessment>> CreateAssessmentAsync([FromForm] AssessmentCreateDto assessmentCreateDto)
        {

            _logger.LogInformation("🎯 استقبال طلب CreateAssessment");
            _logger.LogInformation("📥 الـ DTO: {@Dto}", assessmentCreateDto);

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
                _logger.LogInformation("serviceاستدعاء ال");
                var createdAssessment = await _iassessmentservice.CreateAssesmentAsync(assessmentCreateDto);

                return Ok(createdAssessment);  // ⬅️ هذا المهم!));


            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "⚠️ خطأ في البيانات");
                return BadRequest(new ApiResponse<string>(
                    (int)HttpStatusCode.BadRequest,
                    "Invalid Data",
                    ex.Message
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "🔥 خطأ غير متوقع");

                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ApiResponse<string>(
                        (int)HttpStatusCode.InternalServerError,
                        "An error occurred while creating the assessment.",
                        ex.Message // ⬅️ إرجاع رسالة الخطأ للمطور
                    ));
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("✅ النظام شغال! يا هلا بيك يا محمد");
        }


    }
}


