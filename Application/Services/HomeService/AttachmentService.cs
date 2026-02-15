using Adros.Application.DTOs.Attachment;
using Adros.Application.Interfaces.IService;
using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Course;
using Adros.Shared.Helpers;
using Adros.Shared.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services.HomeService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<AttachmentService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, ILogger<AttachmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        // ===== Get All =====
        public async Task<IReadOnlyList<AttachmentDto>> GetAllAsync()
        {
            var attachments = await _unitOfWork.Repository<Attachment>().ListAllAsync();
            return _mapper.Map<List<AttachmentDto>>(attachments).AsReadOnly();
        }

        // ===== Create =====
        public async Task<AttachmentDto> CreateAsync(CreateAttachmentDto dto)
        {
            var attachment = _mapper.Map<Attachment>(dto);
            attachment.CreatedBy = _currentUserService.UserId;
            attachment.UpdatedBy = _currentUserService.UserId;

            // رفع الملف
            if (dto.FormFile != null && dto.FormFile.Length > 0)
            {
                var fileName = await FileManager.UploadFilePDFAsync(dto.FormFile);
                attachment.Url = $"Uploads/Attachments/{fileName}";
            }

            await _unitOfWork.Repository<Attachment>().AddAsync(attachment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AttachmentDto>(attachment);
        }

        // ===== Get By Id =====
        public async Task<AttachmentDto?> GetAttachmentByIdAsync(Guid id)
        {
            var attachment = await _unitOfWork.Repository<Attachment>().GetByIdAsync(id);
            if (attachment == null) return null;

            return _mapper.Map<AttachmentDto>(attachment);
        }

        // ===== Delete =====
        public async Task<bool> DeleteAttachmentAsync(Guid id)
        {
            var attachment = await _unitOfWork.Repository<Attachment>().GetByIdAsync(id);
            if (attachment == null) return false;

            // حذف الملف من السيرفر
            if (!string.IsNullOrEmpty(attachment.Url))
            {
                FileManager.DeleteFile(Path.GetFileName(attachment.Url), "Attachments");
            }

            _unitOfWork.Repository<Attachment>().Delete(attachment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // ===== Update =====
        public async Task<AttachmentDto?> UpdateAttachmentByIdAsync(Guid id, UpdateAttachmentDto dto)
        {
            var attachment = await _unitOfWork.Repository<Attachment>().GetByIdAsync(id);
            if (attachment == null) return null;

            attachment.Title = dto.Title;

            if (dto.formFile != null && dto.formFile.Length > 0)
            {
                // حذف القديم
                if (!string.IsNullOrEmpty(attachment.Url))
                {
                    FileManager.DeleteFile(Path.GetFileName(attachment.Url), "Attachments");
                }

                // رفع الجديد
                var fileName = await FileManager.UploadFilePDFAsync(dto.formFile);
                attachment.Url = $"Uploads/Attachments/{fileName}";
            }

            attachment.UpdatedAt = DateTime.UtcNow;
            attachment.UpdatedBy = _currentUserService.UserId;

            _unitOfWork.Repository<Attachment>().Update(attachment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AttachmentDto>(attachment);
        }
    }
}
