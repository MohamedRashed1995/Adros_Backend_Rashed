
using Adros.Core.Entities.Course;

namespace Adros.Application.Mappings
{
    public class LessonProfile:BaseProfile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, LessonDto>()
           .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.ApplicationUser.UserName))
           //.ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam != null ? src.Exam.Title : null))
           .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.Select(a => a.Title)))
           .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Videos.Select(v => v.Url)));
        }
    }
}
