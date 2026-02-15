using Adros.Application.DTOs.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface IAttachmentService
    {
        Task<IReadOnlyList<AttachmentDto>> GetAllAsync();
        Task<AttachmentDto?> GetAttachmentByIdAsync(Guid attachmentId);
        Task<AttachmentDto> CreateAsync (CreateAttachmentDto attachment);
        Task<bool> DeleteAttachmentAsync(Guid attachmentid);
        Task<AttachmentDto?> UpdateAttachmentByIdAsync(Guid attachmentId, UpdateAttachmentDto attachment);
        
    }
}
//amr