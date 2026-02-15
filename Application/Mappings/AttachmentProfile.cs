using Adros.Application.DTOs.Attachment;
using Adros.Core.Entities.Course;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Mappings
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment,AttachmentDto>().ReverseMap();
            //CreateMap<Attachment, CreateAttachmentDto>().ReverseMap();
            CreateMap<CreateAttachmentDto, Attachment>();
            CreateMap<UpdateAttachmentDto, Attachment>();

        }
    }
}
