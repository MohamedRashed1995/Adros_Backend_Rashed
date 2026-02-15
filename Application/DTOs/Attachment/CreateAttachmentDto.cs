using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Attachment
{
    public class CreateAttachmentDto
    {
        public string Title { get; set; } = string.Empty;
        public IFormFile FormFile { get; set; }
        public Guid LessonId { get; set; }
    }

}
