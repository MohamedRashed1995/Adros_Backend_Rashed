using Adros.Apis.ApiResponse;
using Adros.Application.DTOs.Attachment;
using Adros.Application.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        // ===== Get All Attachments =====
        [HttpGet]
        public async Task<IActionResult> GetAllAttachments()
        {
            var attachments = await _attachmentService.GetAllAsync();

            // بناء Full URL
            foreach (var att in attachments)
            {
                if (!string.IsNullOrEmpty(att.Url) && !att.Url.StartsWith("http"))
                {
                    att.Url = $"{Request.Scheme}://{Request.Host}/{att.Url.Replace("\\", "/")}";
                }
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "Attachments retrieved successfully",
                Data = attachments
            });
        }

        // ===== Create Attachment =====
        [HttpPost]
        public async Task<IActionResult> CreateAttachment([FromForm] CreateAttachmentDto attachmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attachment = await _attachmentService.CreateAsync(attachmentDto);

            // بناء Full URL
            if (!string.IsNullOrEmpty(attachment.Url))
            {
                attachment.Url = $"{Request.Scheme}://{Request.Host}/{attachment.Url.Replace("\\", "/")}";
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "Attachment Created Successfully",
                Data = attachment
            });
        }

        // ===== Get Attachment By Id =====
        [HttpGet("{attachmentId}")]
        public async Task<IActionResult> GetById(Guid attachmentId)
        {
            var attachment = await _attachmentService.GetAttachmentByIdAsync(attachmentId);
            if (attachment == null) return NotFound();

            if (!string.IsNullOrEmpty(attachment.Url))
            {
                attachment.Url = $"{Request.Scheme}://{Request.Host}/{attachment.Url.Replace("\\", "/")}";
            }

            return Ok(new ApiResponse<AttachmentDto>(200, "Attachment Retrieved Successfully", attachment));
        }

        // ===== Delete Attachment =====
        [HttpDelete("{attachmentId}")]
        public async Task<IActionResult> Delete(Guid attachmentId)
        {
            var success = await _attachmentService.DeleteAttachmentAsync(attachmentId);
            if (!success) return NotFound();

            return Ok(new ApiResponse<AttachmentDto>(200, "Attachment Deleted Successfully"));
        }

        // ===== Update Attachment =====
        [HttpPut("{attachmentId}")]
        public async Task<IActionResult> Update(Guid attachmentId, [FromForm] UpdateAttachmentDto attachmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attachment = await _attachmentService.UpdateAttachmentByIdAsync(attachmentId, attachmentDto);
            if (attachment == null) return NotFound();

            if (!string.IsNullOrEmpty(attachment.Url))
            {
                attachment.Url = $"{Request.Scheme}://{Request.Host}/{attachment.Url.Replace("\\", "/")}";
            }

            return Ok(new ApiResponse<AttachmentDto>(200, "Attachment Updated Successfully", attachment));
        }
    }
}
