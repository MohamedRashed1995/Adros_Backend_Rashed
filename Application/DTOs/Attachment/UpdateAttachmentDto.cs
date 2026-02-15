using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Attachment
{
    public class UpdateAttachmentDto
    {
        public string Title { get; set; }
        public IFormFile formFile { get; set; }
    }
}
