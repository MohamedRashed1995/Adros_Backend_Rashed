using Adros.Application.DTOs.Attachment;
using Adros.Application.DTOs.Lesson;
using Adros.Application.DTOs.Video;
using Adros.Core.Entities.Course;
using AutoMapper;

namespace Adros.Application.Mappings
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
<<<<<<< HEAD
            
            CreateMap<Lesson, LessonDto>()
                //.ForMember(dest => dest.Videos,
                //    opt => opt.MapFrom(src => src.Videos.Select(v => v.Url)))
                //.ForMember(dest => dest.Attachments,
                //    opt => opt.MapFrom(src => src.Attachments.Select(a => a.Url)))
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName));

=======
            // ===============================
            // Lesson
            // ===============================
            CreateMap<Lesson, LessonDto>()
                //.ForMember(dest => dest.Videos,
                //    opt => opt.MapFrom(src => src.Videos))
                .ForMember(dest => dest.Attachments,
                    opt => opt.MapFrom(src => src.Attachments));
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

            CreateMap<LessonCreateDto, Lesson>();
            CreateMap<LessonUpdateDto, Lesson>();

<<<<<<< HEAD
            
=======
            // ===============================
            // Attachment
            // ===============================
            CreateMap<Attachment, AttachmentDto>();

            // ===============================
            // Video
            // ===============================
            CreateMap<Video, VideoDto>();

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        }
    }
}
